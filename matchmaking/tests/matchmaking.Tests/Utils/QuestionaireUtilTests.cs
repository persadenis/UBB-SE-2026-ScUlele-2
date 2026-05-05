using matchmaking.Domain;
using matchmaking.Utils;

namespace matchmaking.Tests.Utils;

public class QuestionaireUtilTests
{
    private static readonly IReadOnlyDictionary<string, LoverType> QuestionTypes = new Dictionary<string, LoverType>
    {
        ["I feel energized after spending time with a group of people."] = LoverType.SOCIAL_EXPLORER,
        ["I enjoy meeting new people frequently."] = LoverType.SOCIAL_EXPLORER,
        ["I prefer social events over staying in."] = LoverType.SOCIAL_EXPLORER,
        ["I easily start conversations with strangers."] = LoverType.SOCIAL_EXPLORER,
        ["I spend a lot of time reflecting on my thoughts and feelings."] = LoverType.DEEP_THINKER,
        ["I prefer deep conversations over small talk."] = LoverType.DEEP_THINKER,
        ["I enjoy spending time alone to recharge."] = LoverType.DEEP_THINKER,
        ["I often analyze my decisions carefully."] = LoverType.DEEP_THINKER,
        ["I like making spontaneous plans."] = LoverType.ADVENTURE_SEEKER,
        ["I enjoy trying new and unfamiliar experiences."] = LoverType.ADVENTURE_SEEKER,
        ["I get bored with routine quickly."] = LoverType.ADVENTURE_SEEKER,
        ["I am comfortable taking risks."] = LoverType.ADVENTURE_SEEKER,
        ["I prefer having a clear plan rather than improvising."] = LoverType.STABILITY_LOVER,
        ["I feel more comfortable with routines."] = LoverType.STABILITY_LOVER,
        ["I like knowing what to expect in advance."] = LoverType.STABILITY_LOVER,
        ["I value consistency in my daily life."] = LoverType.STABILITY_LOVER,
        ["I easily understand how others are feeling."] = LoverType.EMPATHETIC_CONNECTOR,
        ["I value emotional connection in relationships."] = LoverType.EMPATHETIC_CONNECTOR,
        ["I am sensitive to the moods of people around me."] = LoverType.EMPATHETIC_CONNECTOR,
        ["I prioritize harmony in my relationships."] = LoverType.EMPATHETIC_CONNECTOR
    };

    [Fact]
    public void GetQuestions_WhenCalledTwiceWithoutReset_ShouldReturnSameCachedOrder()
    {
        var questionnaireUtil = new QuestionaireUtil();

        var firstCall = questionnaireUtil.GetQuestions();
        var secondCall = questionnaireUtil.GetQuestions();

        Assert.Equal(firstCall, secondCall);
    }

    [Fact]
    public void CalculateLoveType_WhenGetQuestionsWasNotCalled_ShouldThrowInvalidOperationException()
    {
        var questionnaireUtil = new QuestionaireUtil();

        var exception = Assert.Throws<InvalidOperationException>(() => questionnaireUtil.CalculateLoveType(new List<int>()));

        Assert.Equal("GetQuestions() must be called before CalculateLoveType().", exception.Message);
    }

    [Fact]
    public void CalculateLoveType_GivenAdventureFocusedAnswers_ShouldReturnAdventureSeeker()
    {
        var questionnaireUtil = new QuestionaireUtil();
        var questions = questionnaireUtil.GetQuestions();
        var answers = questions
            .Select(question => QuestionTypes[question] == LoverType.ADVENTURE_SEEKER ? 5 : 0)
            .ToList();

        var result = questionnaireUtil.CalculateLoveType(answers);

        Assert.Equal(LoverType.ADVENTURE_SEEKER, result);
    }
}
