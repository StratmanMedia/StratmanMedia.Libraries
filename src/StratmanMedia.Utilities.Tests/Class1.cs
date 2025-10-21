using StratmanMedia.Utilities.Results;

namespace StratmanMedia.Utilities.Tests
{
    public class Class1
    {
        private void Test()
        {
            var result01 = Result.Success();
            var result02 = Result.Success("correlationId");
            var result03 = Result.Error("Error message");
            var result04a = Result.Invalid("Invalid error");
            var result04 = Result.Invalid(new[] { "Invalid error 1", "Invalid error 2" });
            var result05 = Result.NotFound();
            var result06 = Result<Foo>.Success();
            var result07 = Result<Foo>.Success("correlationId");
            var result08 = Result<Foo>.Success(new Foo());
            var result09 = Result<Foo>.Success(new Foo(), "correlationId");
            var result10 = Result<Foo>.Error("Error message");
            var result11 = Result<Foo>.Invalid(new[] { "Invalid error 1", "Invalid error 2" });
            var result12 = Result<Foo>.NotFound();
        }
    }

    public class Foo
    {

    }
}
