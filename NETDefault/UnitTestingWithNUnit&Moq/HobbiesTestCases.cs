﻿using System.Collections;
using NUnit.Framework;

namespace ServiceTests
{
    public class HobbiesTestCases : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new TestCaseData(null, ", hobbies error")
                .SetName($"{nameof(ServiceTests.GetDashboard_WhenHobbiesFound_ItShouldBeReturned)}(null)");

            yield return new TestCaseData(new string[0] { }, ", zero hobbies")
                .SetName($"{nameof(ServiceTests.GetDashboard_WhenHobbiesFound_ItShouldBeReturned)}(zero hobbies)");

            yield return new TestCaseData(new string[1] { "eat" }, ", eat")
                .SetName($"{nameof(ServiceTests.GetDashboard_WhenHobbiesFound_ItShouldBeReturned)}(one hobby)");

            yield return new TestCaseData(new string[2] { "eat", "sleep" }, ", eat, sleep")
                .SetName($"{nameof(ServiceTests.GetDashboard_WhenHobbiesFound_ItShouldBeReturned)}(two hobbies)");

            yield return new TestCaseData(new string[3] { "eat", "sleep", "repeat" }, ", eat, sleep, repeat")
                .SetName($"{nameof(ServiceTests.GetDashboard_WhenHobbiesFound_ItShouldBeReturned)}(three hobbies)");
        }
    }
}