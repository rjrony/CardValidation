namespace CardValidation.Repository
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCard : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Card",
                c => new
                    {
                        CardId = c.Long(nullable: false, identity: true),
                        CardNumber = c.String(nullable: false, maxLength: 16),
                        CardType = c.Int(nullable: false),
                        ExpiryDate = c.String(nullable: false, maxLength: 6),
                    })
                .PrimaryKey(t => t.CardId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Card");
        }
    }
}
