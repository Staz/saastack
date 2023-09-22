﻿using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using Common;
using Common.Extensions;

namespace Application.Interfaces;

/// <summary>
///     Defines options for searching
/// </summary>
public class SearchOptions
{
    public const char SortSignAscending = '+';
    public const char SortSignDescending = '-';
    public const int DefaultLimit = 100;
    public const int MaxLimit = 1000;
    public const int NoLimit = 0;
    public const int NoOffset = -1;

    public static readonly char[] FilterDelimiters =
    {
        ',',
        ';'
    };

    public static readonly char[] SortSigns =
    {
        SortSignAscending,
        SortSignDescending
    };

    public static readonly SearchOptions WithMaxLimit = new() { Limit = MaxLimit };

    public int Limit { get; set; } = DefaultLimit;

    public int Offset { get; set; } = NoOffset;

    public Optional<Sorting> Sort { get; set; }

    public Filtering Filter { get; set; } = new();

    public SearchResults<TResult> ApplyWithMetadata<TResult>(IEnumerable<TResult> results)
    {
        return ApplyWithMetadata(results, SearchOptions<TResult>.DynamicOrderByFunc);
    }

    public void ClearLimitAndOffset()
    {
        Offset = NoOffset;
        Limit = DefaultLimit;
    }

    private SearchResults<TResult> ApplyWithMetadata<TResult>(IEnumerable<TResult> results,
        Func<IEnumerable<TResult>, Sorting, IEnumerable<TResult>> orderByFunc)
    {
        var searchResults = new SearchResults<TResult>
        {
            Metadata = this.ToMetadata()
        };

        var unsorted = results.ToList();
        searchResults.Metadata.Total = unsorted.Count;

        if (Sort.HasValue)
        {
            unsorted = orderByFunc(unsorted, Sort.Value).ToList();
        }

        IEnumerable<TResult> unPaged = unsorted.ToArray();

        if (IsOffSet())
        {
            unPaged = unPaged.Skip(Offset);
        }

        if (IsLimited())
        {
            var limit = Math.Min(MaxLimit, Limit);
            unPaged = unPaged.Take(limit);
        }
        else
        {
            unPaged = unPaged.Take(DefaultLimit);
        }

        searchResults.Results = unPaged.ToList();

        return searchResults;
    }

    private bool IsLimited()
    {
        return Limit > NoLimit;
    }

    private bool IsOffSet()
    {
        return Offset > NoOffset;
    }
}

internal static class SearchOptions<TResult>
{
    public static readonly Func<IEnumerable<TResult>, Sorting, IEnumerable<TResult>> DynamicOrderByFunc =
        (items, sorting) =>
        {
            var by = sorting.By;
            if (by.HasNoValue())
            {
                return items;
            }

            var expression = sorting.Direction switch
            {
                SortDirection.Ascending => $"{by} ascending",
                SortDirection.Descending => $"{by} descending",
                _ => throw new InvalidOperationException(nameof(sorting.Direction))
            };

            var itemsToSort = items.ToArray();
            try
            {
                return itemsToSort
                    .AsQueryable()
                    .OrderBy(expression);
            }
            catch (ParseException)
            {
                // Ignore exception. Possibly an invalid sorting expression?
                return itemsToSort;
            }
        };
}

/// <summary>
///     Defines options for sorting results
/// </summary>
public class Sorting
{
    public Sorting(string by, SortDirection direction = SortDirection.Ascending)
    {
        ArgumentException.ThrowIfNullOrEmpty(by);
        By = by;
        Direction = direction;
    }

    public string By { get; }

    public SortDirection Direction { get; }

    public static Sorting ByField(string field, SortDirection direction = SortDirection.Ascending)
    {
        return new Sorting(field, direction);
    }
}

public enum SortDirection
{
    Ascending = 0,
    Descending = 1
}

/// <summary>
///     Defines options for filtering the fields of results
/// </summary>
public class Filtering
{
    private readonly List<string> _fields = new();

    public Filtering()
    {
    }

    public Filtering(string field)
    {
        ArgumentException.ThrowIfNullOrEmpty(field);
        if (!_fields.Contains(field))
        {
            _fields.Add(field);
        }
    }

    public Filtering(IEnumerable<string> fields)
    {
        foreach (var field in fields)
        {
            if (!_fields.Contains(field))
            {
                _fields.Add(field);
            }
        }
    }

    public IReadOnlyList<string> Fields => _fields;
}