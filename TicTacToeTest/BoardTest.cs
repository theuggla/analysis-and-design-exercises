using System;
using System.Linq;
using System.Collections.Generic;
using Moq;
using Xunit;
using TicTacToe.Model;

namespace TicTacToeTest
{
    public class BoardTest
    {
        private Board sut = new Board();

        [Fact]
        public void GetSquareShouldThrowExceptionIfNoMatchingSquareExists()
        {
            try
            {
                string invalidSquareName = "invalidsquarename";
                Square square = sut.GetSquare(invalidSquareName);
                Assert.True(false, $"got {square.ToString()}");
            }
            catch (NoSuchSquareException)
            {}
        }

        [Fact]
        public void GetSquareShouldReturnSquareWithNameMatchingString()
        {
            sut.NewBoard();
            Square square = sut.GetSquare("a1");
            bool ignoreCase = true;
            Assert.Equal(square.Name.ToString(), "a1", ignoreCase);
        }

        [Fact]
        public void GetSquareShouldReturnSameSquareIfCalledTwice()
        {
            sut.NewBoard();
            Square square = sut.GetSquare("a1");
            Assert.False(square.IsPlayedOn());
            square.PlayOn(PlayerSign.X);;
            Assert.True(square.IsPlayedOn());
            Square square2 = sut.GetSquare("a1");
            Assert.True(square2.IsPlayedOn());
        }

        [Fact]
        public void GetBoardShouldReturnEmptyCollectionOfSquaresWhenNotInitialized()
        {
            Assert.True(sut.GetBoard().Count() == 0);
        }

        [Fact]
        public void NewBoardShouldSetCollectionOfSquares()
        {
            Assert.True(sut.GetBoard().Count() == 0);
            sut.NewBoard();
            Assert.True(sut.GetBoard().Count() == 9);
        }

        public void GetBoardShouldReturnFullCollectionOfSquaresWhenInitialized()
        {
            sut.NewBoard();
            List<Square> expected = GetFullCollectionOfSquares();
            List<Square> actual = (List<Square>) sut.GetBoard();
            expected.OrderBy(x => x.Name);
            actual.OrderBy(x => x.Name);

            if (expected.Count != actual.Count)
            {
                Assert.True(false);
            }

            for (int i = 0; i < expected.Count; i++)
            {
                if (!(expected.ElementAt(i).Equals(actual.ElementAt(i))))
                {
                    Assert.True(false);
                }
            }
        }

        [Fact]
        public void IsEmptyShouldReturnTrueIfNoSquareIsPlayedOn()
        {
            Assert.True(sut.IsEmpty());
        }

        [Fact]
        public void IsEmptyShouldReturnFalseIfASquareIsPlayedOn()
        {
            sut.NewBoard();
            Square square = sut.GetSquare("A1");
            square.PlayOn(PlayerSign.X);
            Assert.False(sut.IsEmpty());
        }

        [Fact]
        public void IsFullShouldReturnTrueIfASquareIsPlayedOn()
        {
            sut.NewBoard();
            foreach (Square square in sut.GetBoard())
            {
                square.PlayOn(PlayerSign.X);
            }

            Assert.True(sut.IsFull());
        }

        private List<Square> GetFullCollectionOfSquares()
        {
            List<Square> squares = new List<Square>();
            foreach (SquareValue squareValue in Enum.GetValues(typeof(SquareValue)))
            {
                squares.Add(new Square(squareValue));
            }
            return squares;
        }
    }
}