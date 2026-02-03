using System;

namespace Impact_Monitor_Program
{
	class ListNode
	{
		private object data;
		private ListNode next;

		public ListNode(object dataValue)
			: this(dataValue, null)
		{
		}

		public ListNode( object dataValue, ListNode nextNode)
		{
			data = dataValue;
			next = nextNode;
		}

		public ListNode Next
		{
			get
			{
				return next;
			} 
			set
			{
				next = value;
			}
		}

		public object Data
		{
			get
			{
				return data;
			}
		}
	}

	public class List
	{
		private ListNode firstNode;
		private ListNode lastNode;
		private string name;

		public List(string listName)
		{
			name = listName;
			firstNode = lastNode = null;
		}

		public List() 
			: this("list")
		{
		}

		public void InsertAtFront(object insertItem)
		{
			if(IsEmpty())
				firstNode = lastNode = new ListNode(insertItem);
			else
				firstNode = new ListNode(insertItem, firstNode);
		}

		public void InsertAtBack(object insertItem)
		{
			if(IsEmpty())
				firstNode = lastNode = new ListNode(insertItem);
			else
				lastNode = lastNode.Next = new ListNode(insertItem);
		}

		public object RemoveFromFront()
		{
			if(IsEmpty())
				throw new EmptyListException(name);
			
			object removeItem = firstNode.Data;

			if(firstNode == lastNode)
				firstNode = lastNode = null;
			else
				firstNode = firstNode.Next;

			return removeItem;
		}

		public object RemoveFromBack()
		{
			if(IsEmpty())
				throw new EmptyListException(name);
			
			object removeItem = lastNode.Data;

			if(firstNode == lastNode)
				firstNode = lastNode = null;
			else
			{
				ListNode current = firstNode;

				while(current.Next != lastNode)
					current = current.Next;

				lastNode = current;
				current.Next = null;
			}

			return removeItem;
		}

		public bool IsEmpty()
		{
			return firstNode == null;
		}

		public class EmptyListException : ApplicationException
		{
			public EmptyListException(string name)
				: base( "The " + " is empty" )
			{
			}
		}
	}		
}