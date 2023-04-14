﻿// <auto-generated />
using System;
using System.Collections.Generic;
using System.Reflection;
using AM.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProductService.Core.Core.Entities;
using ProductService.Core.Core.Enums;

#pragma warning disable 219, 612, 618
#nullable enable

namespace ProductService.Infra
{
    internal partial class ReturnEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType? baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "ProductService.Core.Core.Entities.Return",
                typeof(Return),
                baseEntityType);

            var id = runtimeEntityType.AddProperty(
                "Id",
                typeof(Guid),
                propertyInfo: typeof(EntityBase).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(EntityBase).GetField("<Id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                valueGenerated: ValueGenerated.OnAdd,
                afterSaveBehavior: PropertySaveBehavior.Throw);
            id.AddAnnotation("Relational:ColumnName", "id");
            id.AddAnnotation("Relational:ColumnType", "uuid");
            id.AddAnnotation("Relational:DefaultValueSql", "uuid_generate_v4()");

            var created = runtimeEntityType.AddProperty(
                "Created",
                typeof(DateTime),
                propertyInfo: typeof(EntityBase).GetProperty("Created", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(EntityBase).GetField("<Created>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                valueGenerated: ValueGenerated.OnAdd);
            created.AddAnnotation("Relational:ColumnName", "created");
            created.AddAnnotation("Relational:DefaultValueSql", "now()");

            var customerId = runtimeEntityType.AddProperty(
                "CustomerId",
                typeof(Guid),
                propertyInfo: typeof(Return).GetProperty("CustomerId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Return).GetField("<CustomerId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            customerId.AddAnnotation("Relational:ColumnName", "customer_id");
            customerId.AddAnnotation("Relational:ColumnType", "uuid");

            var note = runtimeEntityType.AddProperty(
                "Note",
                typeof(string),
                propertyInfo: typeof(Return).GetProperty("Note", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Return).GetField("<Note>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            note.AddAnnotation("Relational:ColumnName", "note");

            var productId = runtimeEntityType.AddProperty(
                "ProductId",
                typeof(Guid),
                propertyInfo: typeof(Return).GetProperty("ProductId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Return).GetField("<ProductId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            productId.AddAnnotation("Relational:ColumnName", "product_id");
            productId.AddAnnotation("Relational:ColumnType", "uuid");

            var reason = runtimeEntityType.AddProperty(
                "Reason",
                typeof(ReturnReason),
                propertyInfo: typeof(Return).GetProperty("Reason", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Return).GetField("<Reason>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            reason.AddAnnotation("Relational:ColumnName", "reason");

            var updated = runtimeEntityType.AddProperty(
                "Updated",
                typeof(DateTime?),
                propertyInfo: typeof(EntityBase).GetProperty("Updated", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(EntityBase).GetField("<Updated>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true);
            updated.AddAnnotation("Relational:ColumnName", "updated");

            var key = runtimeEntityType.AddKey(
                new[] { id });
            runtimeEntityType.SetPrimaryKey(key);
            key.AddAnnotation("Relational:Name", "pk_returns");

            var index = runtimeEntityType.AddIndex(
                new[] { id },
                unique: true);
            index.AddAnnotation("Relational:Name", "ix_returns_id");

            var index0 = runtimeEntityType.AddIndex(
                new[] { productId });
            index0.AddAnnotation("Relational:Name", "ix_returns_product_id");

            return runtimeEntityType;
        }

        public static RuntimeForeignKey CreateForeignKey1(RuntimeEntityType declaringEntityType, RuntimeEntityType principalEntityType)
        {
            var runtimeForeignKey = declaringEntityType.AddForeignKey(new[] { declaringEntityType.FindProperty("ProductId")! },
                principalEntityType.FindKey(new[] { principalEntityType.FindProperty("Id")! })!,
                principalEntityType,
                deleteBehavior: DeleteBehavior.Cascade,
                required: true);

            var product = declaringEntityType.AddNavigation("Product",
                runtimeForeignKey,
                onDependent: true,
                typeof(Product),
                propertyInfo: typeof(Return).GetProperty("Product", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Return).GetField("<Product>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            var returns = principalEntityType.AddNavigation("Returns",
                runtimeForeignKey,
                onDependent: false,
                typeof(IEnumerable<Return>),
                propertyInfo: typeof(Product).GetProperty("Returns", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Product).GetField("_returns", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));

            runtimeForeignKey.AddAnnotation("Relational:Name", "fk_returns_products_product_id");
            return runtimeForeignKey;
        }

        public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
        {
            runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
            runtimeEntityType.AddAnnotation("Relational:Schema", "product");
            runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
            runtimeEntityType.AddAnnotation("Relational:TableName", "returns");
            runtimeEntityType.AddAnnotation("Relational:ViewName", null);
            runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

            Customize(runtimeEntityType);
        }

        static partial void Customize(RuntimeEntityType runtimeEntityType);
    }
}
