﻿using NUnit.Framework;

namespace Tests
{
    public class LegalityTests
    {
        //   a  b  c  d  e  f  g  h
        // 8 R, N, B, Q, K, B,  , R 8  BLACK
        // 7 P, P, P, P, P, P, P, P 7
        // 6  ,  ,  ,  ,  ,  ,  ,   6
        // 5  ,  ,  ,  , N,  ,  ,   5
        // 4  ,  ,  ,  ,  ,  ,  ,   4
        // 3  ,  , B,  ,  , P,  ,   3
        // 2 P, P, P, P, P,  , P, P 2
        // 1 R, N, B, Q, K, B, N, R 1  WHITE
        //   a  b  c  d  e  f  g  h
        
        private readonly Piece[,] board;
        
        public LegalityTests()
        {
            this.board = Board.InitialState;
            this.board[2, 5] = new Piece(PieceType.Pawn, PlayerColor.White);   // f3
            this.board[2, 2] = new Piece(PieceType.Bishop, PlayerColor.Black); // Bc3
            this.board[4, 4] = new Piece(PieceType.Knight, PlayerColor.Black); // Ke5
        }
        
        [Theory]
        [TestCase(4, 4, 5, 2)] // Nxf3
        [TestCase(4, 4, 2, 5)] // Nc6
        [TestCase(4, 4, 3, 6)] // Ng4
        public void TestKnightLegal(int originRank, int originFile, int destinationRank, int destinationFile)
        {
            var legality = Legality.CheckMove(
                new Position(originRank, originFile),
                new Position(destinationRank, destinationFile),
                this.board);

            Assert.True(legality);
        }
        
        [Theory]
        [TestCase(4, 4, 6, 5)] // Nxf7
        [TestCase(4, 4, 2, 2)] // Nxc3
        public void TestKnightIllegal(int originRank, int originFile, int destinationRank, int destinationFile)
        {
            var legality = Legality.CheckMove(
                new Position(originRank, originFile),
                new Position(destinationRank, destinationFile),
                this.board);

            Assert.False(legality);
        }

        [Theory]
        [TestCase(2, 2, 1, 1)] // Bxb2
        [TestCase(2, 2, 4, 0)] // Ba6
        public void TestBishopLegal(int originRank, int originFile, int destinationRank, int destinationFile)
        {
            var legality = Legality.CheckMove(
                new Position(originRank, originFile),
                new Position(destinationRank, destinationFile),
                this.board);

            Assert.True(legality);
        }

        [Theory]
        [TestCase(2, 2, 3, 2)] // Bc4
        // [InlineData(2, 2, 0, 0)] // Bxa1
        [TestCase(2, 2, 6, 6)] // Bxg7
        public void TestBishopIllegal(int originRank, int originFile, int destinationRank, int destinationFile)
        {
            var legality = Legality.CheckMove(
                new Position(originRank, originFile),
                new Position(destinationRank, destinationFile),
                this.board);

            Assert.False(legality);
        }
        
        [Theory]
        [TestCase(1, 1, 2, 1)] // b3
        [TestCase(1, 1, 3, 1)] // b4
        [TestCase(2, 5, 3, 5)] // f4
        [TestCase(1, 1, 2, 2)] // bxc3
        public void TestPawnLegal(int originRank, int originFile, int destinationRank, int destinationFile)
        {
            var legality = Legality.CheckMove(
                new Position(originRank, originFile),
                new Position(destinationRank, destinationFile),
                this.board);

            Assert.True(legality);
        }

        [Theory]
        [TestCase(1, 1, 1, 1)] // b3
        [TestCase(1, 1, 4, 1)] // b4
        [TestCase(1, 2, 2, 2)] // c3
        [TestCase(2, 5, 4, 5)] // f5
        [TestCase(2, 5, 1, 5)] // f2
        [TestCase(1, 4, 2, 5)] // exf3
        [TestCase(1, 0, 2, -1)] // a-1
        public void TestPawnIllegal(int originRank, int originFile, int destinationRank, int destinationFile)
        {
            var legality = Legality.CheckMove(
                new Position(originRank, originFile),
                new Position(destinationRank, destinationFile),
                this.board);

            Assert.False(legality);
        }
    }
}