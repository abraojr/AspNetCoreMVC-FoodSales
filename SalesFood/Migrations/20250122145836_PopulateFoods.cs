using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesFood.Migrations
{
    public partial class PopulateFoods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Foods(CategoryId,ShortDescription,LongDescription,InStock,ImageThumbnailUrl,ImageUrl,IsFavoriteFood,Name,Price) VALUES(1,'Bread, hamburger, egg, ham, cheese and potato sticks','Delicious hamburger bun with fried egg, ham and top quality cheese served with potato sticks',1, 'http://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg','http://www.macoratti.net/Imagens/lanches/cheesesalada1.jpg', 0 ,'Cheese Salad', 12.50)");
            migrationBuilder.Sql("INSERT INTO Foods(CategoryId,ShortDescription,LongDescription,InStock,ImageThumbnailUrl,ImageUrl,IsFavoriteFood,Name,Price) VALUES(1,'Bread, ham, mozzarella and tomato','Delicious warm French bread on the griddle with ham and mozzarella served with tomatoes',1,'http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg','http://www.macoratti.net/Imagens/lanches/mistoquente4.jpg',0,'Grilled Ham and Cheese Sandwich', 8.00)");
            migrationBuilder.Sql("INSERT INTO Foods(CategoryId,ShortDescription,LongDescription,InStock,ImageThumbnailUrl,ImageUrl,IsFavoriteFood,Name,Price) VALUES(1,'Bread, hamburger, ham, mozzarella and potato sticks','Special hamburger bun with hamburger from our preparation and ham and mozzarella, served with potato sticks',1,'http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg','http://www.macoratti.net/Imagens/lanches/cheeseburger1.jpg',0,'Cheese Burger', 11.00)");
            migrationBuilder.Sql("INSERT INTO Foods(CategoryId,ShortDescription,LongDescription,InStock,ImageThumbnailUrl,ImageUrl,IsFavoriteFood,Name,Price) VALUES(2,'Wholegrain bread, white cheese, turkey breast, carrots, lettuce, yogurt','Natural wholemeal bread with white cheese, turkey breast and grated carrot with chopped lettuce and natural yogurt',1,'http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg','http://www.macoratti.net/Imagens/lanches/lanchenatural.jpg',1,'Natural Turkey Breast Snack', 15.00)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Foods");
        }
    }
}
