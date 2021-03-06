﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using Vipr.Core.CodeModel;

namespace CSharpWriter
{
    public class Class : AttributableStructure
    {
        private Class()
        {
            Constructors = Enumerable.Empty<Constructor>();
            Fields = Enumerable.Empty<Field>();
            Indexers = Enumerable.Empty<Indexer>();
            Interfaces = Enumerable.Empty<Type>();
            Methods = Enumerable.Empty<Method>();
            NestedClasses = Enumerable.Empty<Class>();
            Properties = Enumerable.Empty<Property>();
        }

        public string AbstractModifier { get; private set; }
        public string AccessModifier { get; private set; }
        public Type BaseClass { get; private set; }
        public IEnumerable<Field> Fields { get; private set; }
        public Identifier Identifier { get; private set; }
        public IEnumerable<Indexer> Indexers { get; private set; }
        public IEnumerable<Type> Interfaces { get; private set; }
        public IEnumerable<Method> Methods { get; private set; }
        public IEnumerable<Property> Properties { get; private set; }
        public IEnumerable<Constructor> Constructors { get; private set; }
        public IEnumerable<Class> NestedClasses { get; private set; }

        public static Class ForFetcher(OdcmClass odcmClass)
        {
            return new Class
            {
                AccessModifier = "internal ",
                BaseClass =
                    new Type(odcmClass.Base == null
                        ? NamesService.GetExtensionTypeName("RestShallowObjectFetcher")
                        : NamesService.GetFetcherTypeName(odcmClass.Base)),
                Constructors = global::CSharpWriter.Constructors.ForFetcher(odcmClass),
                Fields = global::CSharpWriter.Fields.ForFetcher(odcmClass),
                Identifier = NamesService.GetFetcherTypeName(odcmClass),
                Interfaces = global::CSharpWriter.Interfaces.ForFetcher(odcmClass),
                Methods = global::CSharpWriter.Methods.ForFetcher(odcmClass),
                Properties = global::CSharpWriter.Properties.ForFetcher(odcmClass),
            };
        }

        public static Class ForComplex(OdcmClass odcmClass)
        {
            return new Class
            {
                AbstractModifier = odcmClass.IsAbstract ? "abstract " : string.Empty,
                AccessModifier = "public ",
                Constructors = global::CSharpWriter.Constructors.ForComplex(odcmClass),
                BaseClass =
                    new Type(odcmClass.Base == null
                        ? NamesService.GetExtensionTypeName("ComplexTypeBase")
                        : NamesService.GetPublicTypeName(odcmClass.Base)),
                Fields = global::CSharpWriter.Fields.ForComplex(odcmClass),
                Identifier = NamesService.GetConcreteTypeName(odcmClass),
                Properties = global::CSharpWriter.Properties.ForComplex(odcmClass),
            };
        }

        public static Class ForConcrete(OdcmClass odcmClass)
        {
            return new Class
            {
                AbstractModifier = odcmClass.IsAbstract ? "abstract " : string.Empty,
                AccessModifier = "public ",
                Attributes = global::CSharpWriter.Attributes.ForConcrete(odcmClass),
                BaseClass =
                    new Type(odcmClass.Base == null
                        ? NamesService.GetExtensionTypeName("EntityBase")
                        : NamesService.GetConcreteTypeName(odcmClass.Base)),
                Constructors = global::CSharpWriter.Constructors.ForConcrete(odcmClass),
                Fields = global::CSharpWriter.Fields.ForConcrete(odcmClass),
                Identifier = NamesService.GetConcreteTypeName(odcmClass),
                Interfaces = global::CSharpWriter.Interfaces.ForConcrete(odcmClass),
                Methods = global::CSharpWriter.Methods.ForConcrete(odcmClass),
                Properties = global::CSharpWriter.Properties.ForConcrete(odcmClass)
            };
        }

        public static Class ForCollection(OdcmClass odcmClass)
        {
            return new Class
            {
                AccessModifier = "internal ",
                BaseClass = new Type(NamesService.GetExtensionTypeName("QueryableSet"),
                                     new Type(NamesService.GetConcreteInterfaceName(odcmClass))),
                Constructors = global::CSharpWriter.Constructors.ForCollection(odcmClass),
                Interfaces = global::CSharpWriter.Interfaces.ForCollection(odcmClass),
                Identifier = NamesService.GetCollectionTypeName(odcmClass),
                Methods = global::CSharpWriter.Methods.ForCollection(odcmClass),
                Indexers = global::CSharpWriter.Indexers.ForCollection(odcmClass)
            };
        }

        internal static Class ForEntityContainer(OdcmModel odcmModel, OdcmClass odcmContainer)
        {
            return new Class
            {
                AccessModifier = "public ",
                Constructors = global::CSharpWriter.Constructors.ForEntityContainer(odcmContainer),
                Fields = global::CSharpWriter.Fields.ForEntityContainer(odcmContainer),
                Interfaces = global::CSharpWriter.Interfaces.ForEntityContainer(odcmContainer),
                Identifier = NamesService.GetEntityContainerTypeName(odcmContainer),
                NestedClasses = new[] { ForGeneratedEdmModel(odcmModel) },
                Methods = global::CSharpWriter.Methods.ForEntityContainer(odcmContainer),
                Properties = global::CSharpWriter.Properties.ForEntityContainer(odcmContainer)
            };
        }

        internal static Class ForGeneratedEdmModel(OdcmModel odcmModel)
        {
            return new Class
            {
                AbstractModifier = "abstract ",
                AccessModifier = "private ",
                Fields = global::CSharpWriter.Fields.ForGeneratedEdmModel(odcmModel),
                Identifier = new Identifier("", "GeneratedEdmModel"),
                Methods = global::CSharpWriter.Methods.ForGeneratedEdmModel(),
            };
        }
    }
}