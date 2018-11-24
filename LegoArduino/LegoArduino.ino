#include <SPI.h>
#include "MFRC522.h"
#include "SerialCommand.h"

#define RST_PIN           9
#define SS_PIN            10
#define Begin_of_Message  "\x02"
#define End_of_Message    "\x03"

MFRC522 mfrc522(SS_PIN, RST_PIN);
SerialCommand SCmd; 

byte page[5] = {0x23, 0x24, 0x25, 0x26, 0x2B};

void setup()
{
  Serial.begin(9600);
  SPI.begin();
  mfrc522.PCD_Init();
  SCmd.addCommand("/LEGO_ARDUINO", PingPong);
  SCmd.addCommand("/NTAG_HERE", Ntag_Here);
  SCmd.addCommand("/NTAG_UID", Ntag_UID);
  SCmd.addCommand("/NTAG_HALT", Ntag_Halt);
  SCmd.addCommand("/NTAG_READ", Ntag_Read);
  SCmd.addCommand("/NTAG_WRITE", Ntag_Write);
  SCmd.addCommand("/NTAG_AUTH", Ntag_Auth);
  SCmd.addCommand("/NTAG_FULL", Ntag_Full_Read);
}

void loop()
{
  SCmd.readSerial();
}

void PingPong()
{
  Serial.print(Begin_of_Message);
  Serial.print("MFRC522_V001");
  Serial.print(End_of_Message);
}

void Ntag_Halt()
{
  mfrc522.PICC_HaltA();
  mfrc522.PCD_StopCrypto1();

  Serial.print(Begin_of_Message);
  Serial.print("HALT");
  Serial.print(End_of_Message);
}

void Ntag_Here()
{
  Serial.print(Begin_of_Message);
  
    if ( ! mfrc522.PICC_IsNewCardPresent())
    {
       Serial.print("NO");
       Serial.print(End_of_Message);
       return;
    }
    
    // Select one of the cards
    if ( ! mfrc522.PICC_ReadCardSerial())
    {
       Serial.print("NO");
       Serial.print(End_of_Message);
       return;
    }

 Serial.print("YES");
 Serial.print(End_of_Message);
}

void Ntag_UID()
{
  Serial.print(Begin_of_Message);
  for (byte i = 0; i < mfrc522.uid.size; i++)
  {
    Serial.print(mfrc522.uid.uidByte[i] < 0x10 ? "0" : "");
    Serial.print(mfrc522.uid.uidByte[i], HEX);
  } 
  Serial.print(End_of_Message);
}

void Ntag_Read()
{
  MFRC522::StatusCode status;
  byte buffer[18];
  byte size = sizeof(buffer);

  Serial.print(Begin_of_Message);
  
  for(int i = 0;i < 4;i++)
  {
    status = (MFRC522::StatusCode) mfrc522.MIFARE_Read(page[i], buffer, &size);
    if (status != MFRC522::STATUS_OK) 
    {
        Serial.print(Begin_of_Message);
        Serial.print("/ERROR Data: ");
        Serial.print(mfrc522.GetStatusCodeName(status));
        Serial.print(End_of_Message);
    }     
    
    for (byte b = 0; b < 4; b++)
    {
      Serial.print(buffer[b] < 0x10 ? "0" : "");
      Serial.print(buffer[b], HEX);
    }
  }

  Serial.print(End_of_Message);
}

void Ntag_Full_Read()
{
  Serial.print(Begin_of_Message);
  
  for (byte i = 0; i < mfrc522.uid.size; i++)
  {
    Serial.print(mfrc522.uid.uidByte[i] < 0x10 ? "0" : "");
    Serial.print(mfrc522.uid.uidByte[i], HEX);
  } 

 MFRC522::StatusCode status;
  byte buffer[18];
  byte size = sizeof(buffer);

  //Serial.print(Begin_of_Message);
  
  for(int i = 0;i < 4;i++)
  {
    status = (MFRC522::StatusCode) mfrc522.MIFARE_Read(page[i], buffer, &size);
    if (status != MFRC522::STATUS_OK) 
    {
        Serial.print(Begin_of_Message);
        Serial.print("/ERROR/");
        Serial.print(mfrc522.GetStatusCodeName(status));
        Serial.print(End_of_Message);
    }     
    
    for (byte b = 0; b < 4; b++)
    {
      Serial.print(buffer[b] < 0x10 ? "0" : "");
      Serial.print(buffer[b], HEX);
    }
  }
 Serial.print(End_of_Message);
}

void Ntag_Write()
{ 
  byte buffer[20];

  Serial.print(Begin_of_Message);
  Serial.print("/WAIT");
  Serial.print(End_of_Message);

  while(Serial.available() == 0){}

  Serial.readBytes((char *) buffer, 20); 

  MFRC522::StatusCode status;
  
  // Write Data
  for (int i = 0; i < 5; i++)
  {
    status = (MFRC522::StatusCode) mfrc522.MIFARE_Ultralight_Write(page[i], buffer + (i * 4), 4);
    if (status != MFRC522::STATUS_OK)
    {
      Serial.println(Begin_of_Message);
      Serial.print("/ERROR Write Page: 0x");
      Serial.println(page[i], HEX);
      Serial.println(mfrc522.GetStatusCodeName(status));
      Serial.println(End_of_Message);    
    }
    else
    {
      Serial.print("/Sucsess Write Page: 0x");
      //Serial.println(mfrc522.GetStatusCodeName(status));
      Serial.println(page[i], HEX);
    }
  }
  
  if(status == MFRC522::STATUS_OK)
  {
    Serial.print(Begin_of_Message);
    Serial.print("/END_WRITE");
    Serial.print(End_of_Message);
  }
}


void Ntag_Auth()
{
  //byte passWord[4] = {0xA2, 0x21, 0x96, 0x3A};
  byte passWord[4] = {0x40, 0x60, 0x35, 0x00};//40603500
  byte pACK[2];

  //byte buffer[4];

  Serial.print(Begin_of_Message);
  Serial.print("/WAIT");
  Serial.print(End_of_Message);

  while(Serial.available() == 0){}

  Serial.readBytes((char *) passWord, 4);

  
  
  MFRC522::StatusCode status = (MFRC522::StatusCode) mfrc522.PCD_NTAG216_AUTH(passWord, pACK);

  Serial.print(Begin_of_Message);
  
  if (status != MFRC522::STATUS_OK) {

    
  
    Serial.println("/ERROR Auth: ");
    Serial.println(mfrc522.GetStatusCodeName(status));

    byte bufferATQA[2];
    byte bufferSize = sizeof(bufferATQA);
  
    mfrc522.PICC_RequestA(bufferATQA, &bufferSize);
    mfrc522.PICC_Select(&mfrc522.uid);
  }

  Serial.print(End_of_Message);
}

