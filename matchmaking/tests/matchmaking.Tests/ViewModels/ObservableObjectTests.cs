using System.ComponentModel;
using matchmaking.ViewModels;

namespace matchmaking.Tests.ViewModels;

public class ObservableObjectTests
{
    [Fact]
    public void SetProperty_WhenValueChanges_ShouldUpdateValueAndRaisePropertyChanged()
    {
        var observable = new TestObservableObject();
        PropertyChangedEventArgs? receivedArgs = null;
        observable.PropertyChanged += (_, args) => receivedArgs = args;

        var changed = observable.SetName("Alice");

        Assert.True(changed);
        Assert.Equal("Alice", observable.Name);
        Assert.NotNull(receivedArgs);
        Assert.Equal(nameof(TestObservableObject.Name), receivedArgs!.PropertyName);
    }

    [Fact]
    public void SetProperty_WhenValueIsUnchanged_ShouldReturnFalseAndNotRaiseEvent()
    {
        var observable = new TestObservableObject();
        observable.SetName("Alice");
        var eventRaised = false;
        observable.PropertyChanged += (_, _) => eventRaised = true;

        var changed = observable.SetName("Alice");

        Assert.False(changed);
        Assert.False(eventRaised);
    }

    private sealed class TestObservableObject : ObservableObject
    {
        private string _name = string.Empty;

        public string Name => _name;

        public bool SetName(string value)
        {
            return SetProperty(ref _name, value, nameof(Name));
        }
    }
}
