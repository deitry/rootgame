using Xunit;
using System;
using System.Collections.Generic;

namespace RootBase.Tests
{
    public class GameInterfaceFixture : IDisposable
    {
        public GameEngine Engine { get; private set; }
        public GameInterface Interface { get; private set; }

        public GameInterfaceFixture()
        {
            this.Engine = new GameEngine();
            this.Engine.Initialize(GameInterface.DefaultPlayerSet());
            this.Interface = new GameInterface(this.Engine.State);
        }

        public void Dispose() { }
    }

    public class GameInterfaceTests : IClassFixture<GameInterfaceFixture>
    {
        GameInterfaceFixture fixture;

        public GameInterfaceTests(GameInterfaceFixture fixture) { this.fixture = fixture; }

        [Fact]
        void TestGameInterface()
        {
            Assert.True(fixture.Interface.FindSite("Village1") != null);
        }
    }
}
