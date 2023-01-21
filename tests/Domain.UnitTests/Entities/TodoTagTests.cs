using FluentAssertions;
using NUnit.Framework;
using Todo_App.Domain.Entities;
using Todo_App.Domain.Exceptions;
using Todo_App.Domain.ValueObjects;

namespace Todo_App.Domain.UnitTests.Entities;
public class TodoTagTests
{
    [TestCase("TagWith Space")]
    [TestCase("TagWith!")]
    [TestCase("TagWith`")]
    [TestCase("TagWith~")]
    [TestCase("TagWith@")]
    [TestCase("TagWith#")]
    [TestCase("TagWith$")]
    [TestCase("TagWith%")]
    [TestCase("TagWith^")]
    [TestCase("TagWith&")]
    [TestCase("TagWith*")]
    [TestCase("TagWith(")]
    [TestCase("TagWith)")]
    [TestCase("TagWith+")]
    [TestCase("TagWith=")]
    [TestCase("TagWith}")]
    [TestCase("TagWith{")]
    [TestCase("TagWith]")]
    [TestCase("TagWith[")]
    [TestCase(@"TagWith""")]
    [TestCase("TagWith'")]
    [TestCase("TagWith?")]
    [TestCase(@"TagWith\")]
    [TestCase("TagWith>")]
    [TestCase("TagWith<")]
    public void ShouldThrowExceptionGivenNameWithNotSupportedSpecialCharacter(string tag)
    {
        FluentActions.Invoking(() => new TodoTag(tag)).Should().Throw<InvalidTodoTagException>();
    }

    [Test]
    public void ShouldThrowNullReferenceExceptionGivenNameWithNull()
    {
        FluentActions.Invoking(() => new TodoTag(null)).Should().Throw<NullReferenceException>();
    }


}
