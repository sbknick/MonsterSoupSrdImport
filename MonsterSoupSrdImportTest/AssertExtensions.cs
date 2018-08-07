using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

public static class AssertExt
{
    public static Assert And(this Assert assert) => assert;

    public static Assert ElementsAreEqual<T>(this Assert assert, IEnumerable<T> expectedElements, IEnumerable<T> actualElements)
    {
        if (expectedElements == null && actualElements == null)
            return assert;

        var matches = expectedElements.Intersect(actualElements);

        if (matches.Count() != expectedElements.Count())
        {
            var expectedString = $"[{string.Join(", ", expectedElements.Select(x => x.ToString()))}]";
            var actualString = $"[{string.Join(", ", actualElements.Select(x => x.ToString()))}]";
            throw new AssertFailedException(
                "Assert.That.ElementsAreEqual failed. " +
                $"Expected:{expectedString}. Actual:{actualString}."
            );
        }

        return assert;
    }

    public static Assert FieldsAreEqual(this Assert assert, object expected, object actual)
    {
        if (expected == null && actual == null)
            return assert;

        var type = expected.GetType();

        if (!type.IsClass || type == typeof(String))
        {
            Assert.AreEqual(expected, actual);
            return assert;
        }

        if (type != actual.GetType())
        {
            throw new AssertFailedException(
                "Assert.That.FieldsAreEqual failed. " +
                $"Expected type:{type.FullName}. Actual type:{actual.GetType().FullName}."
            );
        }

        var fields = type.GetFields();

        foreach (var field in fields)
        {
            Assert.AreEqual(field.GetValue(expected), field.GetValue(actual));
        }

        return assert;
    }
}
