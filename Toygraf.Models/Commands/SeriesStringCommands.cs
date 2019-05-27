﻿namespace ToyGraf.Models.Commands
{
    using System;

    public abstract class SeriesStringCommand : SeriesPropertyCommand
    {
        protected SeriesStringCommand(int index, string value, Func<Series, string> get, Action<Series, string> set) :
            base(index)
        {
            Value = value;
            Get = get;
            Set = set;
        }

        public new string Value { get => (string)base.Value; set => base.Value = value; }
        protected Func<Series, string> Get;
        protected Action<Series, string> Set;

        protected override void Run(Graph graph)
        {
            var s = Get(graph.Series[Index]);
            Set(graph.Series[Index], Value);
            Value = s;
        }

        public override string ToString() => $"f{Index} {Detail} = \"{Value}\"";
    }

    public class SeriesFormulaCommand : SeriesStringCommand
    {
        public SeriesFormulaCommand(int index, string value) :
            base(index, value,
                s => s.Formula,
                (s, t) => s.Formula = t) { }

        protected override string Detail => "formula";
    }

    public class SeriesTextureCommand : SeriesStringCommand
    {
        public SeriesTextureCommand(int index, string value) :
            base(index, value,
                s => s.Texture,
                (s, t) => s.Texture = t) { }

        protected override string Detail => "texture";
    }

    public class SeriesTexturePathCommand : SeriesStringCommand
    {
        public SeriesTexturePathCommand(int index, string value) :
            base(index, value,
                s => s.TexturePath,
                (s, t) => s.TexturePath = t) { }

        protected override string Detail => "texture path";
    }
}
