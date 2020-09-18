﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Domain.BaseModels
{
	public abstract class DbBase
	{
		/// <summary>
		/// Id
		/// </summary>
		private Guid id;
		[Key]
		public Guid Id {
			get {
				id = id == Guid.Empty ? Guid.NewGuid() : id;
				return id;
			}
			set {
				id = value;
			}
		}

		/// <summary>
		/// CreatedOn
		/// </summary>
		private DateTime? createdOn;
		public DateTime? CreatedOn {
			get {
				createdOn = createdOn == null ? DateTime.UtcNow : createdOn;
				return createdOn;
			}
			set {
				createdOn = value;
			}
		}

		/// <summary>
		/// ModifiedOn
		/// </summary>
		private DateTime? modifiedOn;
		public DateTime? ModifiedOn {
			get {
				modifiedOn = modifiedOn == null ? DateTime.UtcNow : modifiedOn;
				return modifiedOn;
			}
			set {
				modifiedOn = value;
			}
		}
	}
}
