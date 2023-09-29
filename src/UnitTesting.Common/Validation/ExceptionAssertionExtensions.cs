﻿using System.Diagnostics.CodeAnalysis;
using Common.Extensions;
using FluentAssertions.Execution;
using FluentAssertions.Specialized;

namespace UnitTesting.Common.Validation;

// ReSharper disable once CheckNamespace
[ExcludeFromCodeCoverage]
public static class ExceptionAssertionExtensions
{
    internal static bool IsFormattedFrom(string actualExceptionMessage, string expectedMessageWithFormatters)
    {
        var escapedPattern = expectedMessageWithFormatters.Replace("[", "\\[").Replace("]", "\\]").Replace("(", "\\(")
            .Replace(")", "\\)").Replace(".", "\\.").Replace("<", "\\<").Replace(">", "\\>");

        var pattern = escapedPattern.ReplaceWith(@"\{\d+\}", ".*").Replace(" ", @"\s");

        return actualExceptionMessage.IsMatchWith(pattern);
    }

    public static ExceptionAssertions<TException> WithMessageLike<TException>(
        this ExceptionAssertions<TException> @throw, string messageWithFormatters, string because = "",
        params object[] becauseArgs)
        where TException : Exception
    {
        if (!string.IsNullOrEmpty(messageWithFormatters))
        {
            var exception = @throw.Subject.Single();
            var expectedFormat = messageWithFormatters.Replace("{", "{{").Replace("}", "}}");
            Execute.Assertion.BecauseOf(because, becauseArgs)
                .ForCondition(IsFormattedFrom(exception.Message, messageWithFormatters)).UsingLineBreaks.FailWith(
                    $"Expected exception message to match the equivalent of\n\"{expectedFormat}\", but\n\"{exception.Message}\" does not.");
        }

        return new ExceptionAssertions<TException>(@throw.Subject);
    }

    public static async Task<ExceptionAssertions<TException>> WithMessageLike<TException>(
        this Task<ExceptionAssertions<TException>> @throw, string messageWithFormatters, string because = "",
        params object[] becauseArgs)
        where TException : Exception
    {
        if (!string.IsNullOrEmpty(messageWithFormatters))
        {
            var exception = (await @throw).Subject.Single();
            var expectedFormat = messageWithFormatters.Replace("{", "{{").Replace("}", "}}");
            Execute.Assertion.BecauseOf(because, becauseArgs)
                .ForCondition(IsFormattedFrom(exception.Message, messageWithFormatters)).UsingLineBreaks.FailWith(
                    $"Expected exception message to match the equivalent of\n\"{expectedFormat}\", but\n\"{exception.Message}\" does not.");
        }

        return new ExceptionAssertions<TException>((await @throw).Subject);
    }
}