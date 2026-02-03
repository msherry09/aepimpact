using System;

namespace Impact_Monitor_Program
{
	/// <summary>
	/// Summary description for ImpactInfo.
	/// </summary>
	public class ImpactInfo
	{
		private string timeName;
		private string dateName;
		private decimal formatx;
		private decimal formaty;
		private decimal formatz;
		private DateTime currentTime;
		private bool valid;
		private double doublex;
		private double doubley;
		private double doublez;
		private int number;


		/*

		public ImpactInfo( string timeTemp, string dateTemp, decimal formatxTemp, decimal formatyTemp, decimal formatzTemp, DateTime currentTimeTemp, bool validTemp)
		{
			timeName = timeTemp;
			dateName = dateTemp;
			formatx = formatxTemp;
			formaty = formatyTemp;
			formatz = formatzTemp;
			currentTime = currentTimeTemp;
			valid = validTemp;
		}
		*/

		public ImpactInfo()
		{
			timeName = "";
			dateName = "";
			formatx = 0;
			formaty = 0;
			formatz = 0;
			doublex = 0;
			doubley = 0;
			doublez = 0;
			valid = true;
			number = 0;
		}



		public string Time
		{
			get
			{
				return timeName;
			}
			set
			{
				timeName = value;
			}			
		}

		public string Date
		{
			get
			{
				return dateName;
			}
			set
			{
				dateName = value;
			}			
		}

		public decimal FormatX
		{
			get
			{
				return formatx;
			}
			set
			{
				formatx = value;
			}			
		}

		public decimal FormatY
		{
			get
			{
				return formaty;
			}
			set
			{
				formaty = value;
			}			
		}

		public decimal FormatZ
		{
			get
			{
				return formatz;
			}
			set
			{
				formatz = value;
			}			
		}

		public DateTime Current
		{
			get
			{
				return currentTime;
			}
			set
			{
				currentTime = value;
			}			
		}		
		public bool Valid
		{
			get
			{
				return valid;
			}
			set
			{
				valid = value;
			}
		}
		public double DoubleX
		{
			get
			{
				return doublex;
			}
			set
			{
				doublex = value;
			}			
		}

		public double DoubleY
		{
			get
			{
				return doubley;
			}
			set
			{
				doubley = value;
			}			
		}

		public double DoubleZ
		{
			get
			{
				return doublez;
			}
			set
			{
				doublez = value;
			}			
		}

		public int ImpactNumber
		{
			get
			{
				return number;
			}
			set
			{
				number = value;
			}
		}

	}
}
