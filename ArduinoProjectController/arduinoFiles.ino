const int analogInPin = A0;

void setup()
{
	Serial.begin(9600);
	pinMode(13, OUTPUT);
}

void loop()
{
	handleLightSensor();
	handleSerialIn();
	delay(5);
}

void handleLightSensor()
{
	int sensorValue = analogRead(analogInPin);
	Serial.println(sensorValue);
}

String msg = "";
void handleSerialIn()
{
	while (Serial.available() > 0)
	{
		delay(1);
		if (Serial.available() > 0)
		{
			char c = Serial.read();
			msg += c;
		}
	}
	if (msg.length() > 0)
	{
		int arg = -1;
		String msg_text = "";
		for (int i = 0; i < msg.length(); i++)
		{
			msg_text += msg[i];
			if (msg[i] == ' ')
			{
				arg = i;
				break;
			}
		}
		if (msg_text == "ledRed ") //ledRed 10
		{
			String arg_value = "";
			for (int i = arg + 1; i < msg.length(); i++)
			{
				if (msg[i] == ' ' || msg[i] == '\n')
					break;
				arg_value += msg[i];
			}
			switch (arg_value.toInt())
			{
			case 10:
				toggleLED(0);
				break;
			case 11:
				toggleLED(1);
				break;
			default:
				break;
			}
		}
		msg = "";
	}
}
//char message[32];

/*void handleSerialIn()
{
	memset(message, 0, sizeof(message));
	if (Serial.readBytesUntil('\n', message, sizeof(message)))
	{
		while (strchr(message, ' '))
			*strchr(message, ' ') = 0;

		if (strcmp("ledRed", message) == 0)
		{
			char* arg1 = message + strlen(message) + 1;
			int arg1_value = atoi(arg1);
			if (arg1_value == 11)
			{
				toggleLED(0);
			}
			else if (arg1_value == 12)
			{
				toggleLED(1);
			}
		}
	}
}*/
void toggleLED(bool ledON)
{
	digitalWrite(13, ledON);
}
/*char message[32];


"dupa\0arg1\0arg2"
char *arg1_pos = message + strlen(message) + 1 // chyba+1, moze bez
char *arg2_pos = arg1_pos + strlen(arg1_pos) + 1 // chyba+1, moze bez
int argq_val = parseStr2Int(arg1_pos)
# podmienic znak nowej lini na \0
setLed 128\n

if !strchr(message, '\n')
	return dupa
* strchr(message, '\n') = '\0'

if strchr(message, ' ')
  *strchr(message, ' ') = "\0"

	if strcmp('setLed', message) == 0{
		setLed(atoi(message[6 + 1]))

	if strcmp('')
}

void setLed(ind brightness) {
	//
}*/

//if message[32] == \n
//	message[32] = \0
//if message[32] != \0
//  return //problem, zla wiadomosc
//
//	//podmien znak nowej lini na nulla 
//	if (strchr(message, '\n')) {
//		*strchr(message, '\n') = '\0'
//}


//int arg1 = atoi(message[strlen("seetLed ")]);

//char ciag_znakow[] = "ala ma kota"; -> "ala ma kota\0"
/*
void readMessage() {
	memset(message, '\0', sizeof(message));
	if (Serial.readBytesUntil('\n', message, sizeof(message))) {

		int cmd_end = strchr(message, ' ') - message;
		// sprawdzic czy cmd end > sizeof(message)
		if (0 == memcmp(message, "setLed", cmd_end - 1)) {
			if (message[cmd_end + 1] == '1') {
				toggleLED(HIGH);
			}
			else if (message[cmd_end + 1] == '0') {
				toggleLED(LOW);
			}
		}
	}

}*/
