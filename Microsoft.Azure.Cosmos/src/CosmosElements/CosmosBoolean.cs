﻿//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------
namespace Microsoft.Azure.Cosmos.CosmosElements
{
#nullable enable

    using System;
    using Microsoft.Azure.Cosmos.Json;
    using Microsoft.Azure.Cosmos.Query.Core.Monads;

#if INTERNAL
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1600 // Elements should be documented
    public
#else
    internal
#endif
    sealed class CosmosBoolean : CosmosElement, IEquatable<CosmosBoolean>, IComparable<CosmosBoolean>
    {
        private const int TrueHash = 1071096595;
        private const int FalseHash = 1031304189;

        private static readonly CosmosBoolean True = new CosmosBoolean(true);
        private static readonly CosmosBoolean False = new CosmosBoolean(false);

        private CosmosBoolean(bool value)
            : base(CosmosElementType.Boolean)
        {
            this.Value = value;
        }

        public bool Value { get; }

        public override void Accept(ICosmosElementVisitor cosmosElementVisitor) => cosmosElementVisitor.Visit(this);

        public override TResult Accept<TResult>(ICosmosElementVisitor<TResult> cosmosElementVisitor) => cosmosElementVisitor.Visit(this);

        public override TResult Accept<TArg, TResult>(ICosmosElementVisitor<TArg, TResult> cosmosElementVisitor, TArg input) => cosmosElementVisitor.Visit(this, input);

        public override bool Equals(CosmosElement cosmosElement) => cosmosElement is CosmosBoolean cosmosBoolean && this.Equals(cosmosBoolean);

        public bool Equals(CosmosBoolean cosmosBoolean) => this.Value == cosmosBoolean.Value;

        public override int GetHashCode() => this.Value ? TrueHash : FalseHash;

        public int CompareTo(CosmosBoolean cosmosBoolean) => this.Value.CompareTo(cosmosBoolean.Value);

        public static CosmosBoolean Create(bool boolean) => boolean ? CosmosBoolean.True : CosmosBoolean.False;

        public override void WriteTo(IJsonWriter jsonWriter) => jsonWriter.WriteBoolValue(this.Value);

        public static new CosmosBoolean CreateFromBuffer(ReadOnlyMemory<byte> buffer) => CosmosElement.CreateFromBuffer<CosmosBoolean>(buffer);

        public static new CosmosBoolean Parse(string json) => CosmosElement.Parse<CosmosBoolean>(json);

        public static bool TryCreateFromBuffer(
            ReadOnlyMemory<byte> buffer,
            out CosmosBoolean cosmosBoolean) => CosmosElement.TryCreateFromBuffer<CosmosBoolean>(buffer, out cosmosBoolean);

        public static bool TryParse(
            string json,
            out CosmosBoolean cosmosBoolean) => CosmosElement.TryParse<CosmosBoolean>(json, out cosmosBoolean);

        public static new class Monadic
        {
            public static TryCatch<CosmosBoolean> CreateFromBuffer(ReadOnlyMemory<byte> buffer) => CosmosElement.Monadic.CreateFromBuffer<CosmosBoolean>(buffer);

            public static TryCatch<CosmosBoolean> Parse(string json) => CosmosElement.Monadic.Parse<CosmosBoolean>(json);
        }
    }
#if INTERNAL
#pragma warning restore SA1600 // Elements should be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#endif
}
