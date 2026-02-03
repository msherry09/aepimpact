using System;

namespace Impact_Monitor_Program
{
	public class QueueInherritance : List
	{
		public QueueInherritance()
			: base("queue")
		{
		}

		public void Enqueue( object dataValue )
		{
			InsertAtBack(dataValue);
		}

		public object Dequeue()
		{
			return RemoveFromBack();
		}
	}	
}
