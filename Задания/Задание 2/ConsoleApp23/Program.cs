using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumFirst
{
    class Program
    {

        static void Main(string[] args)
        {

            Console.Write("Input filter1=");    //Отдел "Разработка продуктов"
            string p1 = Console.ReadLine();

            Console.Write("Input filter2=");    //Язык "Английский"
            string p2 = Console.ReadLine();

            Console.Write("Input expected number of vacancies=");    //Ожидаемое число вакансий
            //преобразуем строковое значение vacantExp в числовое 
            double vacantExp = double.Parse(Console.ReadLine());

            //создаем драйвер для Chrome
            WebDriver driver = new ChromeDriver();

            //переходим по ссылке
            driver.Navigate().GoToUrl("https://careers.veeam.ru/vacancies");

            //открываем окно на весь экран
            driver.Manage().Window.Maximize();

            //открываем поле "Все отделы"
            driver.FindElement(By.XPath("//button[contains(.,'Все отделы')]")).Click();

            //устанавливаем отбор по p1 (Например, Разработка продуктов)
            var xPathQuery = String.Format("//a[contains(text(),'{0}')]", p1);
            driver.FindElement(By.XPath(xPathQuery)).Click();

            //открываем поле "Все языки"
            driver.FindElement(By.XPath("//button[contains(.,'Все языки')]")).Click();

            //устанавливаем отбор по p2 (Например, Английский)
            var xPathQuery2 = String.Format("//label[contains(.,'{0}')]", p2);
            driver.FindElement(By.XPath(xPathQuery2)).Click();

            //ищем элемент по CssSelector (выбирается элемент, содержащий отобранные вакансии)
            IWebElement element = driver.FindElement(By.CssSelector("div[class='h-100 d-flex flex-column']"));
            
            //ищем элементы по CssSelector (в выбранном элементе формируем список элементов list)
            IList<IWebElement> list = element.FindElements(By.CssSelector("a[class='card card-no-hover card-sm']"));
            
            //посчитываем кол-во элементов в списке list
            int vacantFact = list.Count;

            //условие сравнения фактич.числа вакансий с заданным
            if (vacantFact == vacantExp)
                {
                    //выводим сообщение в консоль
                    Console.WriteLine("Test result: It's OK!");
                }
            else
            {
                //выводим сообщение в консоль
                Console.WriteLine("Test result: FAIL!");
            }

            //закрыть драйвер
            //driver.Close();
        }

        static void WriteString(string input)
        {
            Console.WriteLine(input);
        }
    }

    
}
