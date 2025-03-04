﻿using ToDoApp.Application.Services;

namespace ToDoApp.Application.Dtos
{
	public class GuidData
	{
		public required IGuidGenerator GuidGenerator { get; set; }
		public Guid GetGuid()
		{
			return GuidGenerator.Generate();
		}
	}
}
