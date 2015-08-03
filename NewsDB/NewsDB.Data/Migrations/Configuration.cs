using NewsDB.Models;

namespace NewsDB.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    

    public sealed class Configuration : DbMigrationsConfiguration<NewsDB.Data.NewsDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "NewsDB.Data.NewsDBContext";
        }

        protected override void Seed(NewsDBContext context)
        {
            //check if database is empty and run the Seed() method only database is created for the first time
            if (context.Newses.Any())
            {
                return;
            }

            //fill few students
            News socceer = new News()
            {
                Content = @"�������� /�����/ � ����� ����� ������ ��������� 0:0 � �������-������ �� ������ ��������������� ���� �� ������� �� ������ ���� ������."
            };

            News voleyball = new News()
            {
                Content = "������� ������� �� ����� ��� � ��������� ���������� ���� �� ��������."
            };

            News basketball = new News()
            {
                Content = @"�������������� �� ������ ������ ������ ���� ������ �� ���-������� ����� (MVP) �� ������� ����� � �������� �� ����������� �� �������� � ���, �������� ESPN."
            };

            News sumo = new News()
            {
                Content = "���������� ������ ������ ������ ������� �� ������ � 12-�� ���� �� ������� �� ���� � �����."
            };

            News velo = new News()
            {
                Content = @"����������� ����� ����� ������� 18-��� ���� �� ������������� �������� �� �������, � ���������� ���� ���� ������ ������� �������� �� ����� � �������� ������������ ��������� ��� ��� ����� ������ �� ��� ��� ����� � �����."
            };

            News swimming = new News()
            {
                Content = "��������� �� ����������� ������� �������� ��� ���� �� ���� ������� �� ���������� ���������� �� ������� ���� 2020 ������."
            };

            context.Newses.AddOrUpdate(socceer, voleyball, basketball, sumo, velo, swimming);

            context.SaveChanges();
        }
    }
}
