// Copyright (c) Stride contributors (https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using System;
using System.Collections.Generic;
using Stride.Core.Annotations;
using Stride.Core.IO;
using Stride.Core.Reflection;

namespace Stride.Core.Assets
{
    /// <summary>
    /// Imports a raw asset into the asset system.
    /// </summary>
    [AssemblyScan]
    public interface IAssetImporter
    {
        /// <summary>
        /// Gets an unique identifier to identify the importer. See remarks.
        /// </summary>
        /// <value>The identifier.</value>
        /// <remarks>This identifier is used to recover the importer used for a particular asset. This 
        /// Guid must be unique and stored statically in the definition of an importer. It is used to 
        /// reimport an existing asset with the same importer.
        /// </remarks>
        Guid Id { get; }

        /// <summary>
        /// Gets the name of this importer.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the description of this importer.
        /// </summary>
        /// <value>The description.</value>
        string Description { get; }

        /// <summary>
        /// Gets the order of precedence between the importers, so that an importer can override another one.
        /// </summary>
        /// <value>The order.</value>
        int Order { get; }

        /// <summary>
        /// Gets the supported file extensions (separated by ',' for multiple extensions) by this importer. This is used for display purpose only. The method <see cref="IsSupportingFile"/> is used for matching extensions.
        /// </summary>
        /// <returns>Returns a list of supported file extensions handled by this importer.</returns>
        [NotNull]
        string SupportedFileExtensions { get; }

        /// <summary>
        /// Determines whether this importer is supporting the specified file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns><c>true</c> if this importer is supporting the specified file; otherwise, <c>false</c>.</returns>
        bool IsSupportingFile([NotNull] string filePath);

        /// <summary>
        /// Gets the types of asset that are mainly generated by this importer.
        /// </summary>
        [ItemNotNull]
        IEnumerable<Type> RootAssetTypes { get; }

        /// <summary>
        /// Gets the additional types of asset that can be generated by this importer in complement of the root assets
        /// </summary>
        [ItemNotNull]
        IEnumerable<Type> AdditionalAssetTypes { get; }

        /// <summary>
        /// Gets the default parameters for this importer.
        /// </summary>
        /// <param name="isForReImport"></param>
        /// <value>The supported types.</value>
        AssetImporterParameters GetDefaultParameters(bool isForReImport);

        /// <summary>
        /// Imports a raw assets from the specified path into the specified package.
        /// </summary>
        /// <param name="rawAssetPath">The path to a raw asset on the disk.</param>
        /// <param name="importParameters">The parameters. It is mandatory to call <see cref="GetDefaultParameters"/> and pass the parameters instance here</param>
        IEnumerable<AssetItem> Import(UFile rawAssetPath, AssetImporterParameters importParameters);
    }
}
