
## Задачи (Day 4 - Day 5)
 
1. (deadline 29.03.2019, 24.00) Реализовать класс Transformer, метод TransformToWords которого выполняет преобразование System.Double в его "словестный формат". Например, -23.809 -> "minus two three point eight zero nine". Разработать модульные тесты для тестирования метода. `(done)`.
2. (deadline 29.03.2019, 24.00) Расширить функциональную возможность типа System.Double, реализовав метод Transform, который принимает массив вещественных чисел и трансформирует его в массив строк таким образом, чтобы каждое вещественное число преобразовывалось в его "словестный формат" (LINQ-запросы не использовать!). Например, {-23.809, 0.295, 15.17} -> {"minus two three point eight zero nine", "zero point two nine five", "one five point one seven"}. Разработать модульные тесты для тестирования метода. `(done)`.
3. (deadline 30.03.2019, 24.00) Расширить функциональную возможность типа System.Double, реализовав возможность получения строкового представления вещественного числа в формате IEEE 754. *Готовые классы-конверторы не использовать.* Разработать модульные тесты. Примерные тест-кейсы (для тестирования специальных значений вещественных чисел возможны варианты). `(done)`.
4. (deadline 31.03.2019, 24.00) Разработать неизменяемый класс Polynomial (полином) для работы с многочленами *n*-ой степени от одной переменной вещественного типа (в качестве внутренней структуры для хранения коэффициентов использовать sz-массив). Для разработанного класса переопределить виртуальные методы класса Object; перегрузить операции, допустимые для работы с многочленами (исключая деление многочлена на многочлен), включая "==" и "!=". Разработать модульные тесты для тестирования методов класса. `(done)`.

## Реализация (Done)
1. - [Решение](https://github.com/arinkarus/NET1.S.2019.Chemrukova.04-05/blob/master/DoubleExtensions/Transform.cs)
   - [Тесты](https://github.com/arinkarus/NET1.S.2019.Chemrukova.04-05/blob/master/DoubleExtensions.Tests/TransformTests.cs)
2. - [Решение](https://github.com/arinkarus/NET1.S.2019.Chemrukova.04-05/blob/master/DoubleExtensions/DoubleExtension.cs)
   - [Тесты](https://github.com/arinkarus/NET1.S.2019.Chemrukova.04-05/blob/master/DoubleExtensions.Tests/DoubleExtensionTests.cs)   
3. - [Решение](https://github.com/arinkarus/NET1.S.2019.Chemrukova.04-05/blob/master/DoubleExtensions/DoubleExtension.cs)
   - [Тесты](https://github.com/arinkarus/NET1.S.2019.Chemrukova.04-05/blob/master/DoubleExtensions.Tests/DoubleExtensionTests.cs)   
4. - [Решение](https://github.com/arinkarus/NET1.S.2019.Chemrukova.04-05/blob/master/Polynomial/Polynomial.cs)
   - [Тесты](https://github.com/arinkarus/NET1.S.2019.Chemrukova.04-05/blob/master/Polynomial.Tests/PolynomialTests.cs)
   
