﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CarCareApplication.WebApp.Client.Utility
{
    public static class CollectionUtility
    {
        public static void ReplaceItem<T>(this List<T> col, Func<T, bool> match, T newItem)
        {
            var oldItem = col.FirstOrDefault(i => match(i));
            var oldIndex = col.IndexOf(oldItem);
            col[oldIndex] = newItem;
        }

    }
}
