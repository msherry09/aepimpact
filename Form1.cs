using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace Impact_Monitor_Program
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private StreamReader fileReader;
		private FileStream input;
		private StreamWriter fileWriter;
		private FileStream output;
		private bool searchBool = true;
		private int tempInt;
		private string tempString;
		private int numImpt = 0;
		private char tempChar;
		private System.Windows.Forms.ComboBox comboBox1;
		private int loopCount;
		private string formatString;
		private string formatOutput;
		private System.Windows.Forms.TextBox textBox1;
		private string selection;
		private bool driveBool;
		private bool passBool;
		private int avex;
		private int avey;
		private int avez;
		private int actx;
		private int acty;
		private int actz;
		private double finalx;
		private double finaly;
		private double finalz;						
		private int currentHour;
		private int currentMin;
		private int currentSec;
		private int currentMil;
		private int currentMonth;
		private int currentDay;
		private int currentYear;	
		private DateTime lastTime;		
		private TimeSpan diffTime;
		private double currentLat;
		private double currentLong;
		private double lastLat;
		private double lastLong;		
		private bool firstPass;
		private double distLat;
		private double distLong;
		private double velLat;
		private double velLong;



		//private uint tempUnsignInt;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			driveBool = false;
			passBool = false;
			

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Enabled = false;
			this.button1.Location = new System.Drawing.Point(16, 16);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "Format";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(16, 56);
			this.button2.Name = "button2";
			this.button2.TabIndex = 1;
			this.button2.Text = "Text File";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(16, 96);
			this.button3.Name = "button3";
			this.button3.TabIndex = 2;
			this.button3.Text = "KML File";
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// comboBox1
			// 
			this.comboBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.comboBox1.Items.AddRange(new object[] {
														   "E:",
														   "F:",
														   "G:",
														   "H:",
														   "I:",
														   "J:",
														   "K:",
														   "L:",
														   "M:",
														   "N:",
														   "O:",
														   "P:",
														   "Q:",
														   "R:",
														   "S:",
														   "T:",
														   "U:",
														   "V:",
														   "W:",
														   "X:",
														   "Y:",
														   "Z:"});
			this.comboBox1.Location = new System.Drawing.Point(112, 16);
			this.comboBox1.MaxDropDownItems = 26;
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 21);
			this.comboBox1.TabIndex = 3;
			this.comboBox1.Text = "Select Drive";
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(112, 56);
			this.textBox1.Name = "textBox1";
			this.textBox1.PasswordChar = '*';
			this.textBox1.Size = new System.Drawing.Size(120, 20);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "";
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(256, 134);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Impact Monitor Program";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			int outerLoop;
			int innerLoop;

			
			MessageBox.Show("Do you want to execute " + formatString + "?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);			

			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.EnableRaisingEvents = false;			
			proc.StartInfo.FileName = "format";
			proc.StartInfo.Arguments = selection;
			proc.Start();			
			proc.WaitForExit();			

			
			
			try
			{
				output = new FileStream(formatOutput, FileMode.OpenOrCreate, FileAccess.Write);
				fileWriter = new StreamWriter(output);
			}
			catch(IOException)
			{
				MessageBox.Show("Invalid File Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			//Add code here to format the memory card

			innerLoop = 0; //512
			outerLoop = 0; //1001
			
			while(outerLoop < 2001)
			{
				while(innerLoop < 512)
				{
					fileWriter.Write('#');
					innerLoop++;
				}
				outerLoop++;
				innerLoop = 0;
			}

			//close stuff
			if( output != null)
			{		
				try
				{
					fileWriter.Close();
					output.Close();
				}
				catch(IOException)
				{
					MessageBox.Show("Cannot close file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			MessageBox.Show("Memory Card is ready","Complete",MessageBoxButtons.OK,MessageBoxIcon.Information);
			
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			string latName;
			string longName;
			int actNum;
			double modNum;
			ImpactInfo currentInfo;
			ImpactInfo lastInfo;
			ImpactInfo tempInfo;
			QueueInherritance queue;
			double tempDisLat;
			double tempDisLong;
			double tempLat;
			double tempLong;
			int i;
			bool diffFound;

			//Prompt the user for the location of the file
			OpenFileDialog fileOpener = new OpenFileDialog();
			DialogResult result = fileOpener.ShowDialog();
			//if the user click cancel, exit out of this funtion
			if(result == DialogResult.Cancel)
				return;						
			

			//set the name to the user's file
			string fileName;
			fileName = fileOpener.FileName;

			//if the file does not exist, exit out of the function
			if(fileName == "" || fileName == null)
			{
				MessageBox.Show(fileName + " does not exit", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
				//else open the file into the file streams
			else
			{
				input = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				fileReader = new StreamReader(input);
			}

			//Create an Output file
			SaveFileDialog fileChooser = new SaveFileDialog();	
			fileChooser.DefaultExt = "txt";
			result = fileChooser.ShowDialog();
			//if the user click cancel, exit out of this function
			if(result == DialogResult.Cancel)
				return;

			
			fileName = fileChooser.FileName;
			
			
			fileChooser.CheckFileExists = false;
			
			if(fileName == "" || fileName == null)
			{
				MessageBox.Show("Invalid File Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			else
			{
				try
				{
					output = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
					fileWriter = new StreamWriter(output);
				}
				catch(IOException)
				{
					MessageBox.Show("Invalid File Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			searchBool = true;
			tempInt = fileReader.Read();
			tempString = " ";
			numImpt = 0;			

			while(searchBool)
			{
				if((tempInt != 0)&&(tempInt != 32))
				{
					numImpt = tempInt;
					searchBool = false;
				}
				else
					tempInt = fileReader.Read();
			}

			tempChar = (char) fileReader.Read();
			searchBool = true;
			firstPass = true;
			diffFound = false;
			fileWriter.WriteLine("Number of Impacts: " + numImpt);
			fileWriter.WriteLine(" ");
			fileWriter.WriteLine(" ");
			currentInfo = new ImpactInfo();
			queue = new QueueInherritance();
			i = 1;
			while(numImpt > 0)
			{
				while(searchBool)
				{
					if(tempChar == '$')
						searchBool = false;
					else
						tempChar = (char) fileReader.Read();					
				}
				while(tempChar != ',')
					tempChar = (char) fileReader.Read();

				tempString = "";

				currentHour = 0;
				currentMin = 0;
				currentSec = 0;
				currentMil = 0;
				
				//time
				tempChar = (char) fileReader.Read();
				currentHour = (int) tempChar - 48;
				tempString += tempChar;
				
				tempChar = (char) fileReader.Read();
				currentHour = (currentHour*10) + ((int) tempChar - 48);
				tempString += tempChar;
				

				tempString += ":";
				
				tempChar = (char) fileReader.Read();
				currentMin = (int) tempChar - 48;
				tempString += tempChar;

				tempChar = (char) fileReader.Read();
				currentMin = (currentMin*10) + ((int) tempChar - 48);
				tempString += tempChar;

				tempString += " ";

				tempChar = (char) fileReader.Read();
				currentSec = (int) tempChar - 48;
				tempString += tempChar;

				tempChar = (char) fileReader.Read();
				currentSec = (currentSec*10) + ((int) tempChar - 48);
				tempString += tempChar;

				tempString += (char) fileReader.Read();

				tempChar = (char) fileReader.Read();
				currentMil = (int) tempChar - 48;
				tempString += tempChar;

				tempChar = (char) fileReader.Read();
				currentMil = (currentMil*10) + ((int) tempChar - 48);
				tempString += tempChar;

				tempChar = (char) fileReader.Read();
				currentMil = (currentMil*10) + ((int) tempChar - 48);
				tempString += tempChar;
				
				
				currentInfo.Time = tempString;

				tempChar = (char) fileReader.Read();

				tempChar = (char) fileReader.Read();
				if(tempChar == 'V')
				{
					currentInfo.Valid = false;
					tempChar = (char) fileReader.Read();
					searchBool = true;
					//numImpt--;
					
					
					tempChar = (char) fileReader.Read();
					tempChar = (char) fileReader.Read();
					tempChar = (char) fileReader.Read();
					tempChar = (char) fileReader.Read();
					tempChar = (char) fileReader.Read();
					tempChar = (char) fileReader.Read();
				}
				else
				{

					while(tempChar != ',')
						tempChar = (char) fileReader.Read();


					//Storing Latitude

					
					latName = "";					

					tempChar =  (char) fileReader.Read();
					currentLat = (int) tempChar - 48;
					latName += tempChar;
					
					tempChar =  (char) fileReader.Read();
					currentLat = (currentLat * 10) + (int) tempChar - 48;
					latName += tempChar;
									
				
					latName += ".";


					actNum = fileReader.Read() - 48;
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;		

					tempChar = (char) fileReader.Read();

					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;	
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;				
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;	
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;	
										
					modNum = (double) actNum / 600000;
					currentLat = currentLat + modNum;
					actNum = (int) (modNum * 1000000);
					latName += actNum.ToString();
					
					//getting rid of the ','
					tempChar = (char) fileReader.Read();

					//getting direction
					tempChar = (char) fileReader.Read();

					if(tempChar == 'S')
					{	
						latName = "-" + latName;
						currentLat = currentLat * (-1);
					}
						

					tempChar = (char) fileReader.Read();

					
					
					longName = "";

					tempChar = (char) fileReader.Read();
					if(tempChar != '0')
					{
						currentLong = ((int) tempChar - 48) * 10;
						longName += tempChar;
					}
					tempChar =  (char) fileReader.Read();
					currentLong = (int) tempChar - 48;
					longName += tempChar;
					
					tempChar =  (char) fileReader.Read();
					currentLong = (currentLong * 10) + (int) tempChar - 48;
					longName += tempChar;
									
				
					longName += ".";


					actNum = fileReader.Read() - 48;
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;		

					tempChar = (char) fileReader.Read();

					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;	
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;				
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;	
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;	
										
					modNum = (double) actNum / 600000;
					currentLong = currentLong + modNum;
					actNum = (int) (modNum * 1000000);
					longName += actNum.ToString();
					
					//getting rid of the ','
					tempChar = (char) fileReader.Read();

					//getting direction
					tempChar = (char) fileReader.Read();
					
					if(tempChar == 'W')
					{	
						latName = "-" + latName;
						currentLat = currentLat * (-1);
					}					
					
					

					tempChar = (char) fileReader.Read();

					//past the speed
					tempChar = (char) fileReader.Read();
					while(tempChar != ',')
						tempChar = (char) fileReader.Read();

					//past the course
					tempChar = (char) fileReader.Read();
					while(tempChar != ',')
						tempChar = (char) fileReader.Read();
				}

				//date

				tempString = "";
				currentDay = 0;
				currentMonth = 0;
				currentYear = 0;

				//grabbing day

				tempChar = (char) fileReader.Read();
				currentDay = (int) tempChar - 48;
				tempString += tempChar;

				tempChar = (char) fileReader.Read();
				currentDay = (currentDay*10) + (int) tempChar - 48;
				tempString += tempChar;

				tempString += "/";

				//grabbing month

				tempChar = (char) fileReader.Read();
				currentMonth = (int) tempChar - 48;
				tempString += tempChar;

				tempChar = (char) fileReader.Read();
				currentMonth = (currentMonth*10) + (int) tempChar - 48;
				tempString += tempChar;

				tempString += "/";

				//grabbing year

				tempChar = (char) fileReader.Read();
				currentYear = (int) tempChar - 48;
				tempString += tempChar;

				tempChar = (char) fileReader.Read();
				currentYear = (currentYear*10) + (int) tempChar - 48;
				tempString += tempChar;

				currentInfo.Date = tempString;



				tempChar = (char) fileReader.Read();
				while(tempChar != '*')
					tempChar = (char) fileReader.Read();

				tempChar = (char) fileReader.Read();
				tempChar = (char) fileReader.Read();
				tempChar = (char) fileReader.Read();

				loopCount = 10;
				while(loopCount > 0)
				{
					//tempInt = fileReader.Read();
					tempChar = (char) fileReader.Read();
					
					loopCount--;
				}

				tempChar = (char) fileReader.Read(); //tempInt = fileReader.Read();
				searchBool = true;
				while(searchBool)
				{
					if(tempChar == '$')//tempInt != 0)
						searchBool = false;
					else
						tempChar = (char) fileReader.Read(); //tempInt = fileReader.Read();
				}

				//Start of getting Impact information
				avex = 0;
				avey = 0;
				avez = 0;
				actx = 0;
				acty = 0;
				actz = 0;
				finalx = 0;
				finaly = 0;
				finalz = 0;


				//Average Values
				tempChar = (char) fileReader.Read();
				avex = ((int) tempChar - 65) * 256;
				tempChar = (char) fileReader.Read();
				avex = (((int) tempChar - 65) * 16) + avex;
				tempChar = (char) fileReader.Read();
				avex = ((int) tempChar - 65) + avex;

				tempChar = (char) fileReader.Read();
				avey = ((int) tempChar - 65) * 256;
				tempChar = (char) fileReader.Read();
				avey = (((int) tempChar - 65) * 16) + avey;
				tempChar = (char) fileReader.Read();
				avey = ((int) tempChar - 65) + avey;

				tempChar = (char) fileReader.Read();
				avez = ((int) tempChar - 65) * 256;
				tempChar = (char) fileReader.Read();
				avez = (((int) tempChar - 65) * 16) + avez;
				tempChar = (char) fileReader.Read();
				avez = ((int) tempChar - 65) + avez;

				//Actual Values
				tempChar = (char) fileReader.Read();
				actx = ((int) tempChar - 65) * 256;
				tempChar = (char) fileReader.Read();
				actx = (((int) tempChar - 65) * 16) + actx;
				tempChar = (char) fileReader.Read();
				actx = ((int) tempChar - 65) + actx;

				tempChar = (char) fileReader.Read();
				acty = ((int) tempChar - 65) * 256;
				tempChar = (char) fileReader.Read();
				acty = (((int) tempChar - 65) * 16) + acty;
				tempChar = (char) fileReader.Read();
				acty = ((int) tempChar - 65) + acty;

				tempChar = (char) fileReader.Read();
				actz = ((int) tempChar - 65) * 256;
				tempChar = (char) fileReader.Read();
				actz = (((int) tempChar - 65) * 16) + actz;
				tempChar = (char) fileReader.Read();
				actz = ((int) tempChar - 65) + actz;

				//calculate G's
				currentInfo.DoubleX = (double) (avex - actx)/171;
				currentInfo.DoubleY = (double) (avey - acty)/171;
				currentInfo.DoubleZ = (double) (avez - actz)/171;


				tempChar = (char) fileReader.Read(); //tempInt = fileReader.Read();
				searchBool = true;
				while(searchBool)
				{
					if(tempChar == '$')//tempInt != 0)
						searchBool = false;
					else
						tempChar = (char) fileReader.Read(); //tempInt = fileReader.Read();
				}

				//grabbing the realtime clock for invalid signals
				if(currentInfo.Valid)
				{
					loopCount = 12;
					while(loopCount > 0)
					{
						//tempInt = fileReader.Read();
						tempChar = (char) fileReader.Read();					
						loopCount--;
					}
				}
				else
				{
					tempString = "";
					currentDay = 0;
					currentMonth = 0;
					currentYear = 0;

					//grabbing year

					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentYear = tempInt;

					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentYear = (currentYear*10) + tempInt;

					tempString += "/";

					//grabbing month

					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentMonth = tempInt;

					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentMonth = (currentMonth*10) + tempInt;

					tempString += "/";
					
					//grabbing day

					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentDay = tempInt;

					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentDay = (currentDay*10) + tempInt;


					currentInfo.Date = tempString;

					currentHour = 0;
					currentMin = 0;
					currentSec = 0;
					currentMil = 0;
					tempString = "";
				
					//time
					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentHour = tempInt;

				
					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentHour = (currentHour*10) + tempInt;									

					tempString += ":";
				
					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentMin = tempInt;

				
					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentMin = (currentMin*10) + tempInt;

					tempString += " ";

					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentSec = tempInt;

				
					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentSec = (currentSec*10) + tempInt;
					
					currentMil = 0;
				
				
					currentInfo.Time = tempString;


				}
				
				currentInfo.Current = new DateTime(currentYear, currentMonth, currentDay, currentHour, currentMin, currentSec, currentMil);
				currentInfo.ImpactNumber = i;
				

				//start of flow version 2.0
				if(!(currentInfo.Valid))
				{
					queue.Enqueue(currentInfo);
				}
				else
				{
					if(firstPass)
					{													
						fileWriter.WriteLine("Impact #" + currentInfo.ImpactNumber);
						fileWriter.WriteLine("UTC : " + currentInfo.Time);
						fileWriter.WriteLine("Date (dd/mm/yy): " + currentInfo.Date);
						fileWriter.WriteLine("Latitude: " + currentLat);
						fileWriter.WriteLine("Longitude: " + currentLong);
						fileWriter.WriteLine("Impact X-Direction: " + currentInfo.DoubleX.ToString());
						fileWriter.WriteLine("Impact Y-Direction: " + currentInfo.DoubleY.ToString());
						fileWriter.WriteLine("Impact Z-Direction: " + currentInfo.DoubleZ.ToString());									
						fileWriter.WriteLine(" ");
						firstPass = false;						
					}
					else
					{
						diffTime = lastTime - currentInfo.Current;
						//distance = Math.Sqrt(Math.Pow((currentLat - lastLat),2) + Math.Pow((currentLong - lastLong),2));
						//velocity = distance/diffTime.Milliseconds;
						distLat = currentLat - lastLat;
						distLong = currentLong - lastLong;
						velLat = distLat/diffTime.TotalMilliseconds;
						velLong = distLong/diffTime.TotalMilliseconds;
						if(!(queue.IsEmpty()))
						{
							while(!(queue.IsEmpty()))
							{
								tempInfo = new ImpactInfo();
								tempInfo = (ImpactInfo) queue.Dequeue();
								diffTime = lastTime - tempInfo.Current;
								tempDisLat = velLat * diffTime.TotalMilliseconds;
								tempDisLong = velLong * diffTime.TotalMilliseconds;
								tempLat = tempDisLat + lastLat;
								tempLong = tempDisLong + lastLong;

								fileWriter.WriteLine("Impact #" + currentInfo.ImpactNumber);
								fileWriter.WriteLine("UTC : " + currentInfo.Time);
								fileWriter.WriteLine("Date (yy/mm/dd): " + currentInfo.Date);
								fileWriter.WriteLine("Latitude: " + currentLat);
								fileWriter.WriteLine("Longitude: " + currentLong);
								fileWriter.WriteLine("Impact X-Direction: " + currentInfo.DoubleX.ToString());
								fileWriter.WriteLine("Impact Y-Direction: " + currentInfo.DoubleY.ToString());
								fileWriter.WriteLine("Impact Z-Direction: " + currentInfo.DoubleZ.ToString());									
								fileWriter.WriteLine(" ");	
							}
						}

						//write current info
						fileWriter.WriteLine("Impact #" + currentInfo.ImpactNumber);
						fileWriter.WriteLine("UTC : " + currentInfo.Time);
						fileWriter.WriteLine("Date (dd/mm/yy): " + currentInfo.Date);
						fileWriter.WriteLine("Latitude: " + currentLat);
						fileWriter.WriteLine("Longitude: " + currentLong);
						fileWriter.WriteLine("Impact X-Direction: " + currentInfo.DoubleX.ToString());
						fileWriter.WriteLine("Impact Y-Direction: " + currentInfo.DoubleY.ToString());
						fileWriter.WriteLine("Impact Z-Direction: " + currentInfo.DoubleZ.ToString());									
						fileWriter.WriteLine(" ");						
					}
					lastInfo = currentInfo;					
					lastTime = currentInfo.Current;
					lastLat = currentLat;
					lastLong = currentLong;
				}
				i++;


				/*
				//start of flow verison 1.0
				if(currentInfo.Valid)
				{
					if(firstPass)
					{
						fileWriter.WriteLine("UTC : " + currentInfo.Time);
						fileWriter.WriteLine("Date (dd/mm/yy): " + currentInfo.Date);
						fileWriter.WriteLine("Latitude: " + currentLat);
						fileWriter.WriteLine("Longitude: " + currentLong);
						fileWriter.WriteLine("Impact X-Direction: " + currentInfo.DoubleX.ToString());
						fileWriter.WriteLine("Impact Y-Direction: " + currentInfo.DoubleY.ToString());
						fileWriter.WriteLine("Impact Z-Direction: " + currentInfo.DoubleZ.ToString());									
						fileWriter.WriteLine(" ");
						firstPass = false;
					}
					else
					{
						diffTime = currentInfo.Current.Subtract(lastTime);
						//distance = Math.Sqrt(Math.Pow((currentLat - lastLat),2) + Math.Pow((currentLong - lastLong),2));
						//velocity = distance/diffTime.Milliseconds;
						distLat = currentLat - lastLat;
						distLong = currentLong - lastLong;
						velLat = distLat/diffTime.Milliseconds;
						velLong = distLong/diffTime.Milliseconds;
						diffFound = true;
						if(!(queue.IsEmpty()))
						{
							//empty the queue
							
							while(!(queue.IsEmpty()))
							{
								tempInfo = (ImpactInfo) queue.Dequeue();
							}								
						}
						fileWriter.WriteLine("UTC : " + currentInfo.Time);
						fileWriter.WriteLine("Date (dd/mm/yy): " + currentInfo.Date);
						fileWriter.WriteLine("Latitude: " + currentLat);
						fileWriter.WriteLine("Longitude: " + currentLong);
						fileWriter.WriteLine("Impact X-Direction: " + currentInfo.DoubleX.ToString());
						fileWriter.WriteLine("Impact Y-Direction: " + currentInfo.DoubleY.ToString());
						fileWriter.WriteLine("Impact Z-Direction: " + currentInfo.DoubleZ.ToString());									
						fileWriter.WriteLine(" ");						
					}
					lastInfo = currentInfo;					
					lastTime = currentInfo.Current;
					lastLat = currentLat;
					lastLong = currentLong;
				}
				else
				{
					if(queue.IsEmpty())
					{
						if(diffFound)
						{
							//guess info
							diffTime = currentInfo.Current.Subtract(lastTime);
							tempDisLat = velLat / diffTime.Milliseconds;
							tempDisLong = velLong / diffTime.Milliseconds;
							//tempDistance = velocity / diffTime.Milliseconds;
							slopeLat = distLat/tempDisLat;
							slopeLong = distLong/tempDisLong;
							//slope = distance/tempDistance;
							tempLat = (lastLat/slopeLat) + lastLat;
							tempLong = (lastLong/slopeLong) + lastLong;

							fileWriter.WriteLine("UTC : " + currentInfo.Time);
							fileWriter.WriteLine("Date (yy/mm/dd): " + currentInfo.Date);
							fileWriter.WriteLine("Invalid GPS Signal");
							fileWriter.WriteLine("Latitude: " + tempLat.ToString());
							fileWriter.WriteLine("Longitude: " + tempLong.ToString());
							fileWriter.WriteLine("Impact X-Direction: " + currentInfo.DoubleX.ToString());
							fileWriter.WriteLine("Impact Y-Direction: " + currentInfo.DoubleY.ToString());
							fileWriter.WriteLine("Impact Z-Direction: " + currentInfo.DoubleZ.ToString());									
							fileWriter.WriteLine(" ");
							

						}
						else
						{
							queue.Enqueue(currentInfo);
						}
					}
					else
					{
						queue.Enqueue(currentInfo);
					}
				}
				

				//end of flow
				*/
				/*

				fileWriter.WriteLine("UTC : " + currentInfo.Time);
				fileWriter.WriteLine("Date (dd/mm/yy): " + currentInfo.Date);
				
				
				if(currentInfo.Valid)
				{
					fileWriter.WriteLine("Latitude: " + currentLat);
					fileWriter.WriteLine("Longitude: " + currentLong);
				}
				else
				{
					fileWriter.WriteLine("Invalid GPS Signal");
				}
				
				fileWriter.WriteLine("Impact X-Direction: " + currentInfo.DoubleX.ToString());
				fileWriter.WriteLine("Impact Y-Direction: " + currentInfo.DoubleY.ToString());
				fileWriter.WriteLine("Impact Z-Direction: " + currentInfo.DoubleZ.ToString());
									
				fileWriter.WriteLine(" ");
				*/
				tempChar = (char) fileReader.Read();
				searchBool = true;
				numImpt--;
				currentInfo = new ImpactInfo();				
			}

			if(!(queue.IsEmpty()))
			{
				while(!(queue.IsEmpty()))
				{
					tempInfo = new ImpactInfo();
					tempInfo = (ImpactInfo) queue.Dequeue();
					diffTime = lastTime - tempInfo.Current;
					tempDisLat = velLat * diffTime.TotalMilliseconds;
					tempDisLong = velLong * diffTime.TotalMilliseconds;
					tempLat = tempDisLat + lastLat;
					tempLong = tempDisLong + lastLong;

					fileWriter.WriteLine("Impact #" + currentInfo.ImpactNumber);
					fileWriter.WriteLine("UTC : " + currentInfo.Time);
					fileWriter.WriteLine("Date (yy/mm/dd): " + currentInfo.Date);
					fileWriter.WriteLine("Latitude: " + currentLat);
					fileWriter.WriteLine("Longitude: " + currentLong);
					fileWriter.WriteLine("Impact X-Direction: " + currentInfo.DoubleX.ToString());
					fileWriter.WriteLine("Impact Y-Direction: " + currentInfo.DoubleY.ToString());
					fileWriter.WriteLine("Impact Z-Direction: " + currentInfo.DoubleZ.ToString());									
					fileWriter.WriteLine(" ");	
				}
			}

			//close stuff
			fileReader.Close();
			input.Close();

			if( output != null)
			{
				try
				{
					fileWriter.Close();
					output.Close();
				}
				catch(IOException)
				{
					MessageBox.Show("Cannot close file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			//System.Diagnostics.Process.Start(fileName);
		}

		private void button3_Click(object sender, System.EventArgs e)
		{			
			string fileName;
			int i;
			string dateName;
			string timeName;
			string latName;
			string longName;
			string impaName;
			int actNum;
			double modNum;

			ImpactInfo currentInfo;
			ImpactInfo lastInfo;
			ImpactInfo tempInfo;

			bool diffFound;
			QueueInherritance queue;

			double tempDistance;
			double tempDisLat;
			double tempDisLong;
			double tempLat;
			double tempLong;
			bool updateLoctaion;



			OpenFileDialog fileOpener = new OpenFileDialog();
			DialogResult result = fileOpener.ShowDialog();
			if(result == DialogResult.Cancel)
				return;

			fileName = fileOpener.FileName;
			if(fileName == "" || fileName == null)
			{
				MessageBox.Show(fileName + " does not exist", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			else
			{
				input = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				fileReader = new StreamReader(input);
			}

			//Creating the Output file
			SaveFileDialog fileChooser = new SaveFileDialog();
			fileChooser.DefaultExt = "kml";
			result = fileChooser.ShowDialog();
			if(result == DialogResult.Cancel)
				return;

			
			fileName = fileChooser.FileName;
						

			fileChooser.CheckFileExists = false;

			if(fileName == "" || fileName == null)
			{
				MessageBox.Show(fileName + " does not exist", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			else
			{
				try
				{
					output = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
					fileWriter = new StreamWriter(output);
				}
				catch(IOException)
				{
					MessageBox.Show("Invalid File Name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			//let the fun begins
			//beginning documentation
			//header for the file

			fileWriter.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
			fileWriter.WriteLine("<kml xmlns=\"http://earth.google.com/kml/2.2\">");
			fileWriter.WriteLine("<Document>");

			//code to set color of pushpins
            fileWriter.WriteLine("<Style id=\"sn_blue-pushpin\">");
			fileWriter.WriteLine("<IconStyle>");
			fileWriter.WriteLine("<color>ffff0000</color>");
            fileWriter.WriteLine("<scale>1.1</scale>");
			fileWriter.WriteLine("<Icon>");
            fileWriter.WriteLine("<href>http://maps.google.com/mapfiles/kml/pushpin/blue-pushpin.png</href>");
			fileWriter.WriteLine("</Icon>");
			fileWriter.WriteLine("<hotSpot x=\"20\" y=\"2\" xunits=\"pixels\" yunits=\"pixels\"/>");
			fileWriter.WriteLine("</IconStyle>");
            fileWriter.WriteLine("<ListStyle>");
			fileWriter.WriteLine("</ListStyle>");
			fileWriter.WriteLine("</Style>");

			fileWriter.WriteLine("<Style id=\"sh_ylw-pushpin\">");
            fileWriter.WriteLine("<IconStyle>");
			fileWriter.WriteLine("<color>ff0000ff</color>");
            fileWriter.WriteLine("<scale>1.3</scale>");
            fileWriter.WriteLine("<Icon>");
			fileWriter.WriteLine("<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href>");
			fileWriter.WriteLine("</Icon>");
			fileWriter.WriteLine("<hotSpot x=\"20\" y=\"2\" xunits=\"pixels\" yunits=\"pixels\"/>");
			fileWriter.WriteLine("</IconStyle>");
			fileWriter.WriteLine("<ListStyle>");
			fileWriter.WriteLine("</ListStyle>");
			fileWriter.WriteLine("</Style>");

			fileWriter.WriteLine("<StyleMap id=\"msn_blue-pushpin\">");
			fileWriter.WriteLine("<Pair>");
			fileWriter.WriteLine("<key>normal</key>");
			fileWriter.WriteLine("<styleUrl>#sn_blue-pushpin</styleUrl>");
            fileWriter.WriteLine("</Pair>");
            fileWriter.WriteLine("<Pair>");
			fileWriter.WriteLine("<key>highlight</key>");
			fileWriter.WriteLine("<styleUrl>#sh_blue-pushpin</styleUrl>");
			fileWriter.WriteLine("</Pair>");
			fileWriter.WriteLine("</StyleMap>");

			fileWriter.WriteLine("<Style id=\"sh_blue-pushpin\">");
			fileWriter.WriteLine("<IconStyle>");
			fileWriter.WriteLine("<color>ffff0000</color>");
			fileWriter.WriteLine("<scale>1.3</scale>");
            fileWriter.WriteLine("<Icon>");
			fileWriter.WriteLine("<href>http://maps.google.com/mapfiles/kml/pushpin/blue-pushpin.png</href>");
            fileWriter.WriteLine("</Icon>");
            fileWriter.WriteLine("<hotSpot x=\"20\" y=\"2\" xunits=\"pixels\" yunits=\"pixels\"/>");
			fileWriter.WriteLine("</IconStyle>");
			fileWriter.WriteLine("<ListStyle>");
            fileWriter.WriteLine("</ListStyle>");
            fileWriter.WriteLine("</Style>");

			fileWriter.WriteLine("<StyleMap id=\"msn_ylw-pushpin\">");
			fileWriter.WriteLine("<Pair>");
			fileWriter.WriteLine("<key>normal</key>");
            fileWriter.WriteLine("<styleUrl>#sn_ylw-pushpin</styleUrl>");
			fileWriter.WriteLine("</Pair>");
			fileWriter.WriteLine("<Pair>");
			fileWriter.WriteLine("<key>highlight</key>");
			fileWriter.WriteLine("<styleUrl>#sh_ylw-pushpin</styleUrl>");
			fileWriter.WriteLine("</Pair>");
			fileWriter.WriteLine("</StyleMap>");

			fileWriter.WriteLine("<Style id=\"sn_ylw-pushpin\">");
            fileWriter.WriteLine("<IconStyle>");
			fileWriter.WriteLine("<color>ff0000ff</color>");
			fileWriter.WriteLine("<scale>1.1</scale>");
			fileWriter.WriteLine("<Icon>");
			fileWriter.WriteLine("<href>http://maps.google.com/mapfiles/kml/pushpin/ylw-pushpin.png</href>");
			fileWriter.WriteLine("</Icon>");
			fileWriter.WriteLine("<hotSpot x=\"20\" y=\"2\" xunits=\"pixels\" yunits=\"pixels\"/>");
			fileWriter.WriteLine("</IconStyle>");
			fileWriter.WriteLine("<ListStyle>");
            fileWriter.WriteLine("</ListStyle>");
			fileWriter.WriteLine("</Style>");

			fileWriter.WriteLine("<Style id=\"sn_grn-pushpin\">");
			fileWriter.WriteLine("<IconStyle>");
			fileWriter.WriteLine("<scale>1.1</scale>");
			fileWriter.WriteLine("<Icon>");
			fileWriter.WriteLine("<href>http://maps.google.com/mapfiles/kml/pushpin/grn-pushpin.png</href>");
			fileWriter.WriteLine("</Icon>");
			fileWriter.WriteLine("<hotSpot x=\"20\" y=\"2\" xunits=\"pixels\" yunits=\"pixels\"/>");
			fileWriter.WriteLine("</IconStyle>");
			fileWriter.WriteLine("<ListStyle>");
			fileWriter.WriteLine("</ListStyle>");
			fileWriter.WriteLine("</Style>");

			fileWriter.WriteLine("<StyleMap id=\"msn_grn-pushpin\">");
			fileWriter.WriteLine("<Pair>");
			fileWriter.WriteLine("<key>normal</key>");
			fileWriter.WriteLine("<styleUrl>#sn_grn-pushpin</styleUrl>");
			fileWriter.WriteLine("</Pair>");
			fileWriter.WriteLine("<Pair>");
			fileWriter.WriteLine("<key>highlight</key>");
			fileWriter.WriteLine("<styleUrl>#sh_grn-pushpin</styleUrl>");
			fileWriter.WriteLine("</Pair>");
			fileWriter.WriteLine("</StyleMap>");

			fileWriter.WriteLine("<Style id=\"sh_grn-pushpin\">");
			fileWriter.WriteLine("<IconStyle>");
			fileWriter.WriteLine("<scale>1.3</scale>");
			fileWriter.WriteLine("<Icon>");
			fileWriter.WriteLine("<href>http://maps.google.com/mapfiles/kml/pushpin/grn-pushpin.png</href>");
			fileWriter.WriteLine("</Icon>");
			fileWriter.WriteLine("<hotSpot x=\"20\" y=\"2\" xunits=\"pixels\" yunits=\"pixels\"/>");
			fileWriter.WriteLine("</IconStyle>");
			fileWriter.WriteLine("<ListStyle>");
			fileWriter.WriteLine("</ListStyle>");
			fileWriter.WriteLine("</Style>");

			fileWriter.WriteLine("<Style id=\"sn_wht-pushpin\">");
			fileWriter.WriteLine("<IconStyle>");
			fileWriter.WriteLine("<color>ff686868</color>");
			fileWriter.WriteLine("<scale>1.1</scale>");
			fileWriter.WriteLine("<Icon>");
			fileWriter.WriteLine("<href>http://maps.google.com/mapfiles/kml/pushpin/wht-pushpin.png</href>");
			fileWriter.WriteLine("</Icon>");
			fileWriter.WriteLine("<hotSpot x=\"20\" y=\"2\" xunits=\"pixels\" yunits=\"pixels\"/>");
			fileWriter.WriteLine("</IconStyle>");
			fileWriter.WriteLine("<ListStyle>");
			fileWriter.WriteLine("</ListStyle>");
			fileWriter.WriteLine("</Style>");

			fileWriter.WriteLine("<Style id=\"sh_wht-pushpin\">");
			fileWriter.WriteLine("<IconStyle>");
			fileWriter.WriteLine("<color>ff686868</color>");
			fileWriter.WriteLine("<scale>1.3</scale>");
			fileWriter.WriteLine("<Icon>");
			fileWriter.WriteLine("<href>http://maps.google.com/mapfiles/kml/pushpin/wht-pushpin.png</href>");
			fileWriter.WriteLine("</Icon>");
			fileWriter.WriteLine("<hotSpot x=\"20\" y=\"2\" xunits=\"pixels\" yunits=\"pixels\"/>");
			fileWriter.WriteLine("</IconStyle>");
			fileWriter.WriteLine("<ListStyle>");
			fileWriter.WriteLine("</ListStyle>");
			fileWriter.WriteLine("</Style>");

			fileWriter.WriteLine("<StyleMap id=\"msn_wht-pushpin\">");
			fileWriter.WriteLine("<Pair>");
			fileWriter.WriteLine("<key>normal</key>");
			fileWriter.WriteLine("<styleUrl>#sn_wht-pushpin</styleUrl>");
			fileWriter.WriteLine("</Pair>");
			fileWriter.WriteLine("<Pair>");
			fileWriter.WriteLine("<key>highlight</key>");
			fileWriter.WriteLine("<styleUrl>#sh_wht-pushpin</styleUrl>");
			fileWriter.WriteLine("</Pair>");
			fileWriter.WriteLine("</StyleMap>");
			

			
			//end of documentation

			
			//START READING THE INPUT FILE!!!
			//
			searchBool = true;

			tempInt = fileReader.Read();
			tempString = "";
			numImpt = 0;

			while(searchBool)
			{
				if((tempInt != 0)&&(tempInt != 32))
				{
					numImpt = tempInt;
					searchBool = false;
				}
				else
					tempInt = fileReader.Read();
			}

			tempChar = (char)fileReader.Read();
			searchBool = true;
			firstPass = true;
			diffFound = false;
			updateLoctaion = false;
			i = 1;
			currentInfo = new ImpactInfo();
			queue = new QueueInherritance();
			while(numImpt > 0)
			{
				while(searchBool)
				{
					if(tempChar == '$')
						searchBool = false;
					else
						tempChar = (char)fileReader.Read();
				}
				currentInfo = new ImpactInfo();
			
				dateName = "";
				timeName = "";
				latName = "";
				longName = "";
				currentHour = 0;
				currentMin = 0;
				currentSec = 0;
				currentMil = 0;

				while(tempChar != ',')
					tempChar = (char)fileReader.Read();

				//time

				tempString = "";

				tempChar = (char) fileReader.Read();
				currentHour = (int) tempChar - 48;
				tempString += tempChar;
				
				tempChar = (char) fileReader.Read();
				currentHour = (currentHour*10) + ((int) tempChar - 48);
				tempString += tempChar;
				
				tempString += ":";
				
				tempChar = (char) fileReader.Read();
				currentMin = (int) tempChar - 48;
				tempString += tempChar;

				tempChar = (char) fileReader.Read();
				currentMin = (currentMin*10) + ((int) tempChar - 48);
				tempString += tempChar;

				tempString += " ";

				tempChar = (char) fileReader.Read();
				currentSec = (int) tempChar - 48;
				tempString += tempChar;

				tempChar = (char) fileReader.Read();
				currentSec = (currentSec*10) + ((int) tempChar - 48);
				tempString += tempChar;

				tempString += (char) fileReader.Read();

				tempChar = (char) fileReader.Read();
				currentMil = (int) tempChar - 48;
				tempString += tempChar;

				tempChar = (char) fileReader.Read();
				currentMil = (currentMil*10) + ((int) tempChar - 48);
				tempString += tempChar;

				tempChar = (char) fileReader.Read();
				currentMil = (currentMil*10) + ((int) tempChar - 48);
				tempString += tempChar;			
			
				currentInfo.Time = tempString;

				//getting rid of the ','
				tempChar = (char)fileReader.Read();

				//getting past of the status
				tempChar = (char)fileReader.Read();
				if(tempChar == 'V')
				{
					currentInfo.Valid = false;
					tempChar = (char) fileReader.Read();
					searchBool = true;
					
					tempChar = (char) fileReader.Read();					
					tempChar = (char) fileReader.Read();					
					tempChar = (char) fileReader.Read();					
					tempChar = (char) fileReader.Read();					
					tempChar = (char) fileReader.Read();					
					tempChar = (char) fileReader.Read();					
				}
				else
				{
					currentInfo.Valid = true;
					while(tempChar != ',')
						tempChar = (char)fileReader.Read();

					latName = "";

					//Storing the Latitude
					
					latName = "";					

					tempChar =  (char) fileReader.Read();
					currentLat = (int) tempChar - 48;
					latName += tempChar;
					
					tempChar =  (char) fileReader.Read();
					currentLat = (currentLat * 10) + (int) tempChar - 48;
					latName += tempChar;
									
				
					latName += ".";


					actNum = fileReader.Read() - 48;
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;		

					tempChar = (char) fileReader.Read();

					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;	
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;				
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;	
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;	
										
					modNum = (double) actNum / 600000;
					currentLat = currentLat + modNum;
					actNum = (int) (modNum * 1000000);
					latName += actNum.ToString();
					
					//getting rid of the ','
					tempChar = (char) fileReader.Read();

					//getting direction
					tempChar = (char) fileReader.Read();

					if(tempChar == 'S')
					{	
						latName = "-" + latName;
						currentLat = currentLat * (-1);
					}					

					tempChar = (char) fileReader.Read();

					
					
					longName = "";
					currentLong = 0;

					tempChar = (char) fileReader.Read();
					if(tempChar != '0')
					{
						currentLong = ((int) tempChar - 48) * 10;
						longName += tempChar;
					}
					tempChar =  (char) fileReader.Read();
					currentLong = currentLong + (int) tempChar - 48;
					longName += tempChar;
					
					tempChar =  (char) fileReader.Read();
					currentLong = (currentLong * 10) + (int) tempChar - 48;
					longName += tempChar;
									
				
					longName += ".";


					actNum = fileReader.Read() - 48;
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;		

					tempChar = (char) fileReader.Read();

					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;	
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;				
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;	
					actNum = actNum * 10;
					actNum = (fileReader.Read() - 48) + actNum;	
										
					modNum = (double) actNum / 600000;
					currentLong = currentLong + modNum;
					actNum = (int) (modNum * 1000000);
					longName += actNum.ToString();
					
					//getting rid of the ','
					tempChar = (char) fileReader.Read();

					//getting direction
					tempChar = (char) fileReader.Read();
					
					if(tempChar == 'W')
					{	
						longName = "-" + longName;
						currentLong = currentLong * (-1);
					}																				
					
					tempChar = (char) fileReader.Read();

					//past the speed
					tempChar = (char) fileReader.Read();
					while(tempChar != ',')
						tempChar = (char) fileReader.Read();

					//past the course
					tempChar = (char) fileReader.Read();
					while(tempChar != ',')
						tempChar = (char) fileReader.Read();
				}

				

				//date

				tempString = "";
				currentDay = 0;
				currentMonth = 0;
				currentYear = 0;

				//getting the date
				//dateName = "Date (dd/mm/yy): ";

				//grabbing day

				tempChar = (char) fileReader.Read();
				currentDay = (int) tempChar - 48;
				tempString += tempChar;

				tempChar = (char) fileReader.Read();
				currentDay = (currentDay*10) + (int) tempChar - 48;
				tempString += tempChar;

				tempString += "/";

				//grabbing month

				tempChar = (char) fileReader.Read();
				currentMonth = (int) tempChar - 48;
				tempString += tempChar;

				tempChar = (char) fileReader.Read();
				currentMonth = (currentMonth*10) + (int) tempChar - 48;
				tempString += tempChar;

				tempString += "/";

				//grabbing year

				tempChar = (char) fileReader.Read();
				currentYear = (int) tempChar - 48;
				tempString += tempChar;

				tempChar = (char) fileReader.Read();
				currentYear = (currentYear*10) + (int) tempChar - 48;
				tempString += tempChar;

				currentInfo.Date = tempString;


				tempChar = (char) fileReader.Read();
				while(tempChar != '*')
					tempChar = (char) fileReader.Read();

				tempChar = (char) fileReader.Read();
				tempChar = (char) fileReader.Read();
				tempChar = (char) fileReader.Read();

				loopCount = 10;
				while(loopCount > 0)
				{
					tempInt = fileReader.Read();
					loopCount--;
				}

				tempChar = (char) fileReader.Read(); //tempInt = fileReader.Read();
				searchBool = true;
				while(searchBool)
				{
					if(tempChar == '$')//tempInt != 0)
						searchBool = false;
					else
						tempChar = (char) fileReader.Read(); //tempInt = fileReader.Read();
				}

				//Start of getting Impact information
				avex = 0;
				avey = 0;
				avez = 0;
				actx = 0;
				acty = 0;
				actz = 0;
				finalx = 0;
				finaly = 0;
				finalz = 0;


				//Average Values
				tempChar = (char) fileReader.Read();
				avex = ((int) tempChar - 65) * 256;
				tempChar = (char) fileReader.Read();
				avex = (((int) tempChar - 65) * 16) + avex;
				tempChar = (char) fileReader.Read();
				avex = ((int) tempChar - 65) + avex;

				tempChar = (char) fileReader.Read();
				avey = ((int) tempChar - 65) * 256;
				tempChar = (char) fileReader.Read();
				avey = (((int) tempChar - 65) * 16) + avey;
				tempChar = (char) fileReader.Read();
				avey = ((int) tempChar - 65) + avey;

				tempChar = (char) fileReader.Read();
				avez = ((int) tempChar - 65) * 256;
				tempChar = (char) fileReader.Read();
				avez = (((int) tempChar - 65) * 16) + avez;
				tempChar = (char) fileReader.Read();
				avez = ((int) tempChar - 65) + avez;

				//Actual Values
				tempChar = (char) fileReader.Read();
				actx = ((int) tempChar - 65) * 256;
				tempChar = (char) fileReader.Read();
				actx = (((int) tempChar - 65) * 16) + actx;
				tempChar = (char) fileReader.Read();
				actx = ((int) tempChar - 65) + actx;

				tempChar = (char) fileReader.Read();
				acty = ((int) tempChar - 65) * 256;
				tempChar = (char) fileReader.Read();
				acty = (((int) tempChar - 65) * 16) + acty;
				tempChar = (char) fileReader.Read();
				acty = ((int) tempChar - 65) + acty;

				tempChar = (char) fileReader.Read();
				actz = ((int) tempChar - 65) * 256;
				tempChar = (char) fileReader.Read();
				actz = (((int) tempChar - 65) * 16) + actz;
				tempChar = (char) fileReader.Read();
				actz = ((int) tempChar - 65) + actz;
				
				if((avex == 0)&&(avey == 0)&&(avez == 0))
				{
					if((actx == 0)&&(acty == 0)&&(actz == 0))
						updateLoctaion = true;
				}

				//calculate G's
				finalx = (double) (avex - actx)/171;
				finaly = (double) (avey - acty)/171;
				finalz = (double) (avez - actz)/171;

				currentInfo.FormatX = (decimal) finalx;
				currentInfo.FormatY = (decimal) finaly;
				currentInfo.FormatZ = (decimal) finalz;

				impaName = "Impact X-Direction: " + finalx.ToString() + ", Impact Y-Direction: " + finaly.ToString() + ", Impact Z-Direction: " + finalz.ToString();
				tempChar = (char) fileReader.Read(); //tempInt = fileReader.Read();
				searchBool = true;
				while(searchBool)
				{
					if(tempChar == '$')//tempInt != 0)
						searchBool = false;
					else
						tempChar = (char) fileReader.Read(); //tempInt = fileReader.Read();
				}

				//grabbing the realtime clock for invalid signals
				if(currentInfo.Valid)
				{
					loopCount = 12;
					while(loopCount > 0)
					{
						//tempInt = fileReader.Read();
						tempChar = (char) fileReader.Read();					
						loopCount--;
					}
				}
				else
				{
					tempString = "";
					currentDay = 0;
					currentMonth = 0;
					currentYear = 0;

					//grabbing year

					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentYear = tempInt;

					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentYear = (currentYear*10) + tempInt;

					tempString += "/";

					//grabbing month

					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentMonth = tempInt;

					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentMonth = (currentMonth*10) + tempInt;

					tempString += "/";
					
					//grabbing day

					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentDay = tempInt;

					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentDay = (currentDay*10) + tempInt;


					currentInfo.Date = tempString;

					currentHour = 0;
					currentMin = 0;
					currentSec = 0;
					currentMil = 0;
					tempString = "";
				
					//time
					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentHour = tempInt;

				
					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentHour = (currentHour*10) + tempInt;									

					tempString += ":";
				
					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentMin = tempInt;

				
					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentMin = (currentMin*10) + tempInt;

					tempString += " ";

					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentSec = tempInt;

				
					tempChar = (char) fileReader.Read();
					tempInt = ((int) tempChar - 65);
					tempString += tempInt.ToString();
					currentSec = (currentSec*10) + tempInt;
					
					currentMil = 0;
				
				
					currentInfo.Time = tempString;


				}
				
				currentInfo.Current = new DateTime(currentYear, currentMonth, currentDay, currentHour, currentMin, currentSec, currentMil);
				currentInfo.ImpactNumber = i;

				//start of flow version 2.0
				if(!(currentInfo.Valid))
				{
					queue.Enqueue(currentInfo);
				}
				else
				{
					if(firstPass)
					{
						fileWriter.WriteLine("<Placemark>");			
						//fileWriter.WriteLine("<name>Impact " + currentInfo.ImpactNumber + "</name>");
						fileWriter.WriteLine("<name>Initial Reading </name>");
						fileWriter.WriteLine("<description> Date (dd/mm/yy): " + currentInfo.Date);
						fileWriter.WriteLine("UTC : " + currentInfo.Time);
						//fileWriter.WriteLine("Impact X-Direction: {0:F} g ", currentInfo.FormatX);
						//fileWriter.WriteLine("Impact Y-Direction: {0:F} g ", currentInfo.FormatY);
						//fileWriter.WriteLine("Impact Z-Direction: {0:F} g ", currentInfo.FormatZ);
						fileWriter.WriteLine(" </description>");
						fileWriter.WriteLine("<styleUrl>#msn_grn-pushpin</styleUrl>");
						fileWriter.WriteLine("<Point>");
						fileWriter.WriteLine("<coordinates>" + currentLong + "," + currentLat + ",0</coordinates>");
						fileWriter.WriteLine("</Point>");
						fileWriter.WriteLine("</Placemark>");						
						firstPass = false;
						i--;

					}
					else
					{
						diffTime = lastTime - currentInfo.Current;
						//distance = Math.Sqrt(Math.Pow((currentLat - lastLat),2) + Math.Pow((currentLong - lastLong),2));
						//velocity = distance/diffTime.Milliseconds;
						distLat = currentLat - lastLat;
						distLong = currentLong - lastLong;
						velLat = distLat/diffTime.TotalMilliseconds;
						velLong = distLong/diffTime.TotalMilliseconds;
						if(!(queue.IsEmpty()))
						{
							while(!(queue.IsEmpty()))
							{
								tempInfo = new ImpactInfo();
								tempInfo = (ImpactInfo) queue.Dequeue();
								diffTime = lastTime - tempInfo.Current;
								tempDisLat = velLat * diffTime.TotalMilliseconds;
								tempDisLong = velLong * diffTime.TotalMilliseconds;
								tempLat = tempDisLat + lastLat;
								tempLong = tempDisLong + lastLong;

								fileWriter.WriteLine("<Placemark>");			
								fileWriter.WriteLine("<name>Impact " + tempInfo.ImpactNumber + "</name>");
								fileWriter.WriteLine("<description> Date (yy/mm/dd): " + tempInfo.Date);
								fileWriter.WriteLine("UTC : " + tempInfo.Time);
								fileWriter.WriteLine("Impact X-Direction: {0:F} g ", tempInfo.FormatX);
								fileWriter.WriteLine("Impact Y-Direction: {0:F} g ", tempInfo.FormatY);
								fileWriter.WriteLine("Impact Z-Direction: {0:F} g ", tempInfo.FormatZ);
								fileWriter.WriteLine(" </description>");
								fileWriter.WriteLine("<styleUrl>#msn_wht-pushpin</styleUrl>");
								fileWriter.WriteLine("<Point>");
								fileWriter.WriteLine("<coordinates>" + tempLong + "," + tempLat + ",0</coordinates>");
								fileWriter.WriteLine("</Point>");
								fileWriter.WriteLine("</Placemark>");
							}
						}
						
						if(updateLoctaion)
						{
							fileWriter.WriteLine("<Placemark>");			
							fileWriter.WriteLine("<name>Updating Location </name>");
							fileWriter.WriteLine("<description> Date (dd/mm/yy): " + currentInfo.Date);
							fileWriter.WriteLine("UTC : " + currentInfo.Time);
							fileWriter.WriteLine(" </description>");
							fileWriter.WriteLine("<styleUrl>#msn_grn-pushpin</styleUrl>");
							fileWriter.WriteLine("<Point>");
							fileWriter.WriteLine("<coordinates>" + currentLong + "," + currentLat + ",0</coordinates>");
							fileWriter.WriteLine("</Point>");
							fileWriter.WriteLine("</Placemark>");						
							i--;
						}
						else
						{

							//write current info
							fileWriter.WriteLine("<Placemark>");			
							fileWriter.WriteLine("<name>Impact " + currentInfo.ImpactNumber + "</name>");
							fileWriter.WriteLine("<description> Date (dd/mm/yy): " + currentInfo.Date);
							fileWriter.WriteLine("UTC : " + currentInfo.Time);
							fileWriter.WriteLine("Impact X-Direction: {0:F} g ", currentInfo.FormatX);
							fileWriter.WriteLine("Impact Y-Direction: {0:F} g ", currentInfo.FormatY);
							fileWriter.WriteLine("Impact Z-Direction: {0:F} g ", currentInfo.FormatZ);
							fileWriter.WriteLine(" </description>");
							fileWriter.WriteLine("<styleUrl>#msn_ylw-pushpin</styleUrl>");
							fileWriter.WriteLine("<Point>");
							fileWriter.WriteLine("<coordinates>" + currentLong + "," + currentLat + ",0</coordinates>");
							fileWriter.WriteLine("</Point>");
							fileWriter.WriteLine("</Placemark>");
						}
					}
					lastInfo = currentInfo;					
					lastTime = currentInfo.Current;
					lastLat = currentLat;
					lastLong = currentLong;
				}
				i++;

				//end of flow version 2.0

				/*
					//start of flow version 1.0
				if(currentInfo.Valid)
				{
					if(firstPass)
					{
						fileWriter.WriteLine("<Placemark>");			
						fileWriter.WriteLine("<name>Impact " + currentInfo.ImpactNumber + "</name>");
						fileWriter.WriteLine("<description> Date (dd/mm/yy): " + currentInfo.Date);
						fileWriter.WriteLine("UTC : " + currentInfo.Time);
						fileWriter.WriteLine("Impact X-Direction: {0:F} g ", currentInfo.FormatX);
						fileWriter.WriteLine("Impact Y-Direction: {0:F} g ", currentInfo.FormatY);
						fileWriter.WriteLine("Impact Z-Direction: {0:F} g ", currentInfo.FormatZ);
						fileWriter.WriteLine(" </description>");
						fileWriter.WriteLine("<Point>");
						fileWriter.WriteLine("<coordinates>" + currentLong + "," + currentLat + ",0</coordinates>");
						fileWriter.WriteLine("</Point>");
						fileWriter.WriteLine("</Placemark>");						
						firstPass = false;
						i++;
					}
					
					else
					{
						diffTime = lastTime - currentInfo.Current;
						//distance = Math.Sqrt(Math.Pow((currentLat - lastLat),2) + Math.Pow((currentLong - lastLong),2));
						//velocity = distance/diffTime.Milliseconds;
						distLat = currentLat - lastLat;
						distLong = currentLong - lastLong;
						velLat = distLat/diffTime.TotalMilliseconds;
						velLong = distLong/diffTime.TotalMilliseconds;
						diffFound = true;
						if(!(queue.IsEmpty()))
						{
							//empty the queue
							
							while(!(queue.IsEmpty()))
							{
								tempInfo = (ImpactInfo) queue.Dequeue();
								i++;
							}								
						}
						fileWriter.WriteLine("<Placemark>");			
						fileWriter.WriteLine("<name>Impact " + currentInfo.ImpactNumber + "</name>");
						fileWriter.WriteLine("<description> Date (dd/mm/yy): " + currentInfo.Date);
						fileWriter.WriteLine("UTC : " + currentInfo.Time);
						fileWriter.WriteLine("Impact X-Direction: {0:F} g ", currentInfo.FormatX);
						fileWriter.WriteLine("Impact Y-Direction: {0:F} g ", currentInfo.FormatY);
						fileWriter.WriteLine("Impact Z-Direction: {0:F} g ", currentInfo.FormatZ);
						fileWriter.WriteLine(" </description>");
						fileWriter.WriteLine("<Point>");
						fileWriter.WriteLine("<coordinates>" + currentLong + "," + currentLat + ",0</coordinates>");
						fileWriter.WriteLine("</Point>");
						fileWriter.WriteLine("</Placemark>");
						i++;
					}
					lastInfo = currentInfo;					
					lastTime = currentInfo.Current;
					lastLat = currentLat;
					lastLong = currentLong;
				}
				else
				{
					if(queue.IsEmpty())
					{
						if(diffFound)
						{
							//guess info
							diffTime = lastTime - currentInfo.Current;
							tempDisLat = velLat * diffTime.TotalMilliseconds;
							tempDisLong = velLong * diffTime.TotalMilliseconds;
							//tempDistance = velocity / diffTime.Milliseconds;
							//slopeLat = distLat/tempDisLat;
							//slopeLong = distLong/tempDisLong;
							//slope = distance/tempDistance;
							tempLat = tempDisLat + lastLat;
							tempLong = tempDisLong + lastLong;

							fileWriter.WriteLine("<Placemark>");			
							fileWriter.WriteLine("<name>Impact " + currentInfo.ImpactNumber + "</name>");
							fileWriter.WriteLine("<description> Date (yy/mm/dd): " + currentInfo.Date);
							fileWriter.WriteLine("UTC : " + currentInfo.Time);
							fileWriter.WriteLine("Impact X-Direction: {0:F} g ", currentInfo.FormatX);
							fileWriter.WriteLine("Impact Y-Direction: {0:F} g ", currentInfo.FormatY);
							fileWriter.WriteLine("Impact Z-Direction: {0:F} g ", currentInfo.FormatZ);
							fileWriter.WriteLine(" </description>");
							fileWriter.WriteLine("<Point>");
							fileWriter.WriteLine("<coordinates>" + tempLong + "," + tempLat + ",0</coordinates>");
							fileWriter.WriteLine("</Point>");
							fileWriter.WriteLine("</Placemark>");
							i++;
							

						}
						else
						{
							queue.Enqueue(currentInfo);
						}
					}
					else
					{
						queue.Enqueue(currentInfo);
					}
				}
				

				//end of flow verison 1.0
				*/
				tempChar = (char) fileReader.Read();
				searchBool = true;
				updateLoctaion = false;
				numImpt--;
			}
			if(!(queue.IsEmpty()))
			{
				while(!(queue.IsEmpty()))
				{
					tempInfo = (ImpactInfo) queue.Dequeue();
					diffTime = lastTime - tempInfo.Current;
					tempDisLat = velLat * diffTime.TotalMilliseconds;
					tempDisLong = velLong * diffTime.TotalMilliseconds;
					tempLat = tempDisLat + lastLat;
					tempLong = tempDisLong + lastLong;

					fileWriter.WriteLine("<Placemark>");			
					fileWriter.WriteLine("<name>Impact " + tempInfo.ImpactNumber + "</name>");
					fileWriter.WriteLine("<description> Date (yy/mm/dd): " + tempInfo.Date);
					fileWriter.WriteLine("UTC : " + tempInfo.Time);
					fileWriter.WriteLine("Impact X-Direction: {0:F} g ", tempInfo.FormatX);
					fileWriter.WriteLine("Impact Y-Direction: {0:F} g ", tempInfo.FormatY);
					fileWriter.WriteLine("Impact Z-Direction: {0:F} g ", tempInfo.FormatZ);
					fileWriter.WriteLine(" </description>");
					fileWriter.WriteLine("<styleUrl>#msn_wht-pushpin</styleUrl>");
					fileWriter.WriteLine("<Point>");
					fileWriter.WriteLine("<coordinates>" + tempLong + "," + tempLat + ",0</coordinates>");
					fileWriter.WriteLine("</Point>");
					fileWriter.WriteLine("</Placemark>");
				}
			}
		
			fileWriter.WriteLine("</Document>");
			fileWriter.WriteLine("</kml>");

			//end stuff
			fileReader.Close();
			input.Close();											
														
			if( output != null)
			{		
				try
				{
					fileWriter.Close();
					output.Close();
				}
				catch(IOException)
				{
					MessageBox.Show("Cannot close file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			MessageBox.Show("File has converted.\nOpening Google Earth","Complete",MessageBoxButtons.OK,MessageBoxIcon.Information,0,0);
		//Go to Start->Run and opens up Google Earth
		System.Diagnostics.Process.Start(fileName);
		}

		private void comboBox1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			switch(comboBox1.SelectedIndex)
			{
				case 0:
					selection = "E:";					
					break;
				case 1:
					selection = "F:";
					break;
				case 2:
					selection = "G:";
					break;
				case 3:
					selection = "H:";
					break;
				case 4:
					selection = "I:";
					break;
				case 5:
					selection = "J:";
					break;
				case 6:
					selection = "K:";
					break;
				case 7:
					selection = "L:";
					break;
				case 8:
					selection = "M:";
					break;
				case 9:
					selection = "N:";
					break;
				case 10:
					selection = "O:";
					break;
				case 11:
					selection = "P:";
					break;
				case 12:
					selection = "Q:";
					break;
				case 13:
					selection = "R:";
					break;
				case 14:
					selection = "S:";
					break;
				case 15:
					selection = "T:";
					break;
				case 16:
					selection = "U:";
					break;
				case 17:
					selection = "V:";
					break;
				case 18:
					selection = "W:";
					break;
				case 19:
					selection = "X:";
					break;
				case 20:
					selection = "Y:";
					break;
				case 21:
					selection = "Z:";
					break;
			}
			formatString = "format " + selection;
			formatOutput = selection + "\\" + "New Text Document.txt";
			if(!driveBool)
				driveBool = true;			
			if(passBool && driveBool)
				button1.Enabled = true;
			else
				button1.Enabled = false;
		}

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
			string password;

			password = "biscuits";
			if(textBox1.Text == password)
				passBool = true;
			else
				passBool = false;

			if(passBool && driveBool)
				button1.Enabled = true;
			else
				button1.Enabled = false;
		}
	}
}


