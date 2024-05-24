using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Project.ViewModels {
	public class PaginatedList<T> : IEnumerable<T> {
		public List<T> Items { get; set; }
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
		public bool HasPrev => PageIndex > 1;
		public bool HasNext => PageIndex < TotalPages;

		public PaginatedList(List<T> items, int totalPages, int pageIndex, int pageSize) {
			Items = items;
			TotalPages = totalPages;
			PageIndex = pageIndex;
			PageSize = pageSize;
		}

		public static PaginatedList<T> Create(IQueryable<T> query, int pageIndex, int pageSize) {
			if (pageIndex <= 0) pageIndex = 1;
			int totalPages = (int)Math.Ceiling(query.Count() / (double)pageSize);

			var items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

			return new PaginatedList<T>(items, totalPages, pageIndex, pageSize);
		}

		public IEnumerator<T> GetEnumerator() {
			return Items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
