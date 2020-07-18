const byte numChars = 32;
char receivedChars[numChars]; // an array to store the received data
int lengthOfData;
boolean newData = false;
const int relayPins[2] = {2, 3};
const int ledPins[2]   = {4, 5};

void setup() {  
  Serial.begin(9600);
  while(!Serial){}
  pinMode(relayPins[0], OUTPUT);
  pinMode(relayPins[1], OUTPUT);
  pinMode(ledPins[0], OUTPUT);
  pinMode(ledPins[1], OUTPUT);
  Serial.println("Ardunio Serial is on");
}

void loop() {
    recvWithEndMarker();
    if (newData == true) {
    Serial.print("Recived serial message: ");
    Serial.println(receivedChars);
    if (lengthOfData == 2) {
      int relayPin = 0;
      int ledPin = 0;
      if (receivedChars[0] == '0') {
        Serial.println("Left relay command");
        relayPin = relayPins[0];
        ledPin = ledPins[0];
      } else if (receivedChars[0] == '1') {
        Serial.println("Right relay command");
        relayPin = relayPins[1];
        ledPin = ledPins[1];
      } else {
        Serial.println("Unkonwn relay address");
        return;
      }
      if (receivedChars[1] == '0') {
        Serial.println("Turing Off pin " + String(relayPin));
        digitalWrite(relayPin, HIGH);
        digitalWrite(ledPin, LOW);
        
      } else if (receivedChars[1] == '1') {
        Serial.println("Turing On pin " + String(relayPin));
        digitalWrite(relayPin, LOW);
        digitalWrite(ledPin, HIGH);
      } else {
        Serial.println("Unkonwn relay state");
        return;
      }
    } else {
        Serial.println("Message was not correct size: " + String(lengthOfData));
    }
     
     newData = false;
   }
}

String getNextSerialMessage() {
  String readString;
  String Q;
  while(!Serial.available()){}
  while (Serial.available()) {
    delay(1);  //delay to allow buffer to fill 
    if (Serial.available() >0) {
      char c = Serial.read();  //gets one byte from serial buffer
      if (isControl(c)) {
        //'Serial.println("it's a control character");
        break;
      }
      readString += c; //makes the string readString    
     }
  }
  return readString;
}
void recvWithEndMarker() {
 static byte ndx = 0;
 lengthOfData = 0;
 char endMarker = '>';
 char rc;
 
 // if (Serial.available() > 0) {
 while (Serial.available() > 0 && newData == false) {
   rc = Serial.read();
  
   if (rc != endMarker) {
    receivedChars[ndx] = rc;
    ndx++;
    if (ndx >= numChars) {
      ndx = numChars - 1;
    }
   }
   else {
    receivedChars[ndx] = '\0'; // terminate the string
    lengthOfData = ndx;
    ndx = 0;
    newData = true;
   }
 }
}
