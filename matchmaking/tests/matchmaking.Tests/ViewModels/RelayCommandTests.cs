using matchmaking.ViewModels;

namespace matchmaking.Tests.ViewModels;

public class RelayCommandTests
{
    [Fact]
    public void Execute_WhenCalled_ShouldInvokeAction()
    {
        var executed = false;
        var command = new RelayCommand(() => executed = true);

        command.Execute(null);

        Assert.True(executed);
    }

    [Fact]
    public void CanExecute_WhenPredicateReturnsFalse_ShouldReturnFalse()
    {
        var command = new RelayCommand(() => { }, () => false);

        var canExecute = command.CanExecute(null);

        Assert.False(canExecute);
    }

    [Fact]
    public void NotifyCanExecuteChanged_WhenCalled_ShouldRaiseEvent()
    {
        var command = new RelayCommand(() => { });
        var eventRaised = false;
        command.CanExecuteChanged += (_, _) => eventRaised = true;

        command.NotifyCanExecuteChanged();

        Assert.True(eventRaised);
    }

    [Fact]
    public void GenericCommand_GivenValidParameter_ShouldEvaluatePredicateAndExecuteAction()
    {
        var capturedValue = 0;
        var command = new RelayCommand<int>(value => capturedValue = value, value => value > 10);

        var canExecuteWithSmallValue = command.CanExecute(10);
        var canExecuteWithLargeValue = command.CanExecute(11);
        command.Execute(11);

        Assert.False(canExecuteWithSmallValue);
        Assert.True(canExecuteWithLargeValue);
        Assert.Equal(11, capturedValue);
    }

    [Fact]
    public void GenericCommand_GivenInvalidParameterType_ShouldIgnoreExecution()
    {
        var capturedValue = 0;
        var command = new RelayCommand<int>(value => capturedValue = value);

        var canExecute = command.CanExecute("11");
        command.Execute("11");

        Assert.False(canExecute);
        Assert.Equal(0, capturedValue);
    }
}
