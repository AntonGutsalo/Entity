using System;
using System.Data.Entity.Migrations;

namespace Entity.Migrations
{
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brunch",
                c => new
                    {
                        BrunchID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        City = c.String(nullable: false, maxLength: 100),
                        Adress = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.BrunchID);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 100),
                        Sex = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                        BrunchID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Brunch", t => t.BrunchID)
                .Index(t => t.BrunchID);
            
            CreateTable(
                "dbo.Contract",
                c => new
                    {
                        ContractID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        TypeOfInsuranceID = c.Int(nullable: false),
                        BranchID = c.Int(nullable: false),
                        InsuranceAgentID = c.Int(nullable: false),
                        Term = c.Int(nullable: false),
                        ClientID = c.Int(nullable: false),
                        Refusal = c.String(maxLength: 4000),
                    })
                .PrimaryKey(t => t.ContractID)
                .ForeignKey("dbo.Client", t => t.ClientID)
                .ForeignKey("dbo.TypeOfInsurance", t => t.TypeOfInsuranceID)
                .ForeignKey("dbo.Workers", t => t.InsuranceAgentID)
                .Index(t => t.TypeOfInsuranceID)
                .Index(t => t.InsuranceAgentID)
                .Index(t => t.ClientID);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientID = c.Int(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.String(nullable: false, maxLength: 20),
                        IdentificationCode = c.Int(nullable: false),
                        Coefficient = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ClientID);
            
            CreateTable(
                "dbo.InsuredEvents",
                c => new
                    {
                        EventsID = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, storeType: "money"),
                        Status = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        Date = c.DateTime(nullable: false),
                        ContractID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventsID)
                .ForeignKey("dbo.Contract", t => t.ContractID)
                .Index(t => t.ContractID);
            
            CreateTable(
                "dbo.TypeOfInsurance",
                c => new
                    {
                        TypeOfInsuranceID = c.Int(nullable: false),
                        InsuranceType = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.TypeOfInsuranceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Workers", "BrunchID", "dbo.Brunch");
            DropForeignKey("dbo.Contract", "InsuranceAgentID", "dbo.Workers");
            DropForeignKey("dbo.Contract", "TypeOfInsuranceID", "dbo.TypeOfInsurance");
            DropForeignKey("dbo.InsuredEvents", "ContractID", "dbo.Contract");
            DropForeignKey("dbo.Contract", "ClientID", "dbo.Client");
            DropIndex("dbo.InsuredEvents", new[] { "ContractID" });
            DropIndex("dbo.Contract", new[] { "ClientID" });
            DropIndex("dbo.Contract", new[] { "InsuranceAgentID" });
            DropIndex("dbo.Contract", new[] { "TypeOfInsuranceID" });
            DropIndex("dbo.Workers", new[] { "BrunchID" });
            DropTable("dbo.TypeOfInsurance");
            DropTable("dbo.InsuredEvents");
            DropTable("dbo.Client");
            DropTable("dbo.Contract");
            DropTable("dbo.Workers");
            DropTable("dbo.Brunch");
        }
    }
}
