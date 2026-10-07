using Microsoft.AspNetCore.Mvc;
using wt_lab4_4mvc_zhiznevskaya.Models;

namespace wt_lab4_4mvc_zhiznevskaya.Controllers;

public class CarController : Controller
{
    // Тестовая коллекция — без БД, прямо в коде
    private static readonly List<Car> Cars = new()
    {
        new Car { Id = 1, Brand = "Toyota", Model = "Camry", Year = 2022, PricePerDay = 5000, IsAvailable = true },
        new Car { Id = 2, Brand = "BMW", Model = "X5", Year = 2023, PricePerDay = 12000, IsAvailable = true },
        new Car { Id = 3, Brand = "Volvo", Model = "FH16", Year = 2021, PricePerDay = 18000, IsAvailable = false },
        new Car { Id = 4, Brand = "Kia", Model = "Rio", Year = 2021, PricePerDay = 3500, IsAvailable = true },
        new Car { Id = 5, Brand = "Mercedes-Benz", Model = "GLE", Year = 2024, PricePerDay = 15000, IsAvailable = true },
    };

    public IActionResult Index()
    {
        return View(Cars);
    }
        // GET: /Car/Create — показать форму
    public IActionResult Create()
    {
        return View();
    }
        // GET: /Car/Details/5
    public IActionResult Details(int id)
    {
        var car = Cars.FirstOrDefault(c => c.Id == id);
        if (car == null) return NotFound();
        return View(car);
    }

    // GET: /Car/Edit/5
    public IActionResult Edit(int id)
    {
        var car = Cars.FirstOrDefault(c => c.Id == id);
        if (car == null) return NotFound();
        return View(car);
    }

    // POST: /Car/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Car car)
    {
        if (!ModelState.IsValid) return View(car);

        var existing = Cars.FirstOrDefault(c => c.Id == id);
        if (existing == null) return NotFound();

        existing.Brand = car.Brand;
        existing.Model = car.Model;
        existing.Year = car.Year;
        existing.PricePerDay = car.PricePerDay;
        existing.IsAvailable = car.IsAvailable;

        return RedirectToAction(nameof(Index));
    }

    // GET: /Car/Delete/5
    public IActionResult Delete(int id)
    {
        var car = Cars.FirstOrDefault(c => c.Id == id);
        if (car == null) return NotFound();
        return View(car);
    }

    // POST: /Car/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var car = Cars.FirstOrDefault(c => c.Id == id);
        if (car != null) Cars.Remove(car);
        return RedirectToAction(nameof(Index));
    }

    // POST: /Car/Create — принять данные и добавить автомобиль
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Car car)
    {
        if (!ModelState.IsValid)
            return View(car);

        // Автоматически присваиваем новый Id
        int newId = Cars.Count == 0 ? 1 : Cars.Max(c => c.Id) + 1;
        car.Id = newId;

        // Добавляем в коллекцию (List<Car> Cars)
        Cars.Add(car);

        return RedirectToAction(nameof(Index));
    }
}