using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Omniture.SiteCatalyst {
    /// <summary>Migration creates database and tables for siteCatalyst including all it's parts.</summary>
    public class Migrations : DataMigrationImpl {

        /// <summary>
        /// First method to run for creating SiteCatalyst.
        /// </summary>
        /// <returns>Returns an version int 1</returns>
        public int Create() {
            SchemaBuilder.CreateTable("SiteCatalystPartRecord", table =>
                table.ContentPartRecord()
                    .Column<string>("PageName")
                    .Column<string>("PageType"));

            ContentDefinitionManager.AlterPartDefinition("SiteCatalystPart", builder => builder.Attachable());

            ContentDefinitionManager.AlterTypeDefinition("SiteCatalyst", builder =>
               builder.WithPart("SiteCatalystPart"));
            return 1;
        }

        /// <summary>
        /// Second Method to run and update the version.
        /// </summary>
        /// <returns>Updated version from 1 to 2</returns>
        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterTypeDefinition("SiteCatalyst",
                builder => builder
                    .WithPart("CommonPart")
                    .WithPart("WidgetPart")
                    .WithSetting("Stereotype", "Widget"));

            return 2;
        }
    }
}