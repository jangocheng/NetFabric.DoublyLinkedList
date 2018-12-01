﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace NetFabric.Tests
{
    public class ReverseTests
    {
        public static TheoryData<IEnumerable<int>, IEnumerable<int>> Data =>
            new TheoryData<IEnumerable<int>, IEnumerable<int>>
            {
                { new int[] { },                new int[] { } },
                { new int[] { 1 },              new int[] { 1 } },
                { new int[] { 1, 2, 3, 4, 5 },  new int[] { 5, 4, 3, 2, 1 } },
            };

        [Theory]
        [MemberData(nameof(Data))]
        public void Reverse(IEnumerable<int> collection, IEnumerable<int> expected)
        {
            // Arrange
            var list = new DoubleLinkedList<int>(collection);

            // Act
            var result = list.Reverse();

            // Assert
            result.EnumerateForward().Should().Equal(expected);
            result.EnumerateReversed().Should().Equal(collection);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void ReverseInPlace(IEnumerable<int> collection, IEnumerable<int> expected)
        {
            // Arrange
            var list = new DoubleLinkedList<int>(collection);
            var version = list.Version;

            // Act
            list.ReverseInPlace();

            // Assert
            if(list.Count < 2)
                list.Version.Should().Be(version);
            else
                list.Version.Should().NotBe(version);
            list.EnumerateForward().Should().Equal(expected);
            list.EnumerateReversed().Should().Equal(collection);
        }
    }
}