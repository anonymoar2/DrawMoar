# DrawMoar
Raster and vector graphics editor for creating cartoons

В ветке Master сейчас находится версия редактора за 17.04.17


  		
*Тут должна быть UML диаграмма, запилите её кто-нибудь пожалуйста*
  

**Функциональные требования:**


Список фич: 
  
    1. Содание нового мультика выбранного размера в пределах 256х144 - 3840х2160 +
    2. Создание нового пустого кадра +
    3. Возмжность сделать копию выбранного кадра 
    4. Удаление кадра 
    5. Изменение порядка кадров 
  
    6. Создание нового слоя пустого слоя, векторного или растрового +
    6.1 Возможность переименовать слой 
    7. Возможность сделать копию выбранного слоя 
    8. Удаление слоя 
    8.1 Изменение порядка слоёв 
    8.2 Возможность сделать слой невидимым 
    9. Растрирование векторного слоя
    10. Слияние слоёв по правилам:  R + R = R, V + R = R, V + V = V 
  
    11. Сохранение промежуточного результата в \*your_cartoon\*.drm (наш собственный формат)
    12. Экспорт кадра в *name_of_your_image*.PNG 
    12.1 Экспорт кадра в *name_of_your_image*.JPG
    13. Экспорт мультика в *name_of_your_cartoon*.mp4 +
    13.1 Экспорт мультика в *name_of_your_cartoon*.avi +
  
    14. Добавление своего PNG или JPEG изображения на кадр (в качестве нового растрового слоя) +
    15. Добавление фигуры из списка стандартных +
    16. Преобразования над содержимым слоя (Translate, rotate, scale) +
    17. Рисование кистью из набора стандартных кистей +
    18. Пипетка 
    19. Ластик +
  
    20. Выбрать фрагмент слоя (select)
    21. Преобразования над выбранным фрагментов слоя (Translate, rotate, scale)
    23. Инструмент "Заливка"
  
    24. Отмена изменений в количестве n
    25. Добавление текста
    26. Использование текстур (styles в фотошопе) для векторных слоёв
    27. "Облака" для диалогов и мыслей, трансформирующиеся под текст
    28. Эффекты и фильтры для кадра
    28.1 Генерация кадров +
    28.2 Зацикливание кадров +
    
    29. Сохраненные слои и работа с ними 
    
    30. Эффекты дождя/снега
  
Дополнительные фичи которые мы возможно реализуем: (никогда )) )

    29. *Выбор языка интерфекса (Russian, English, German)
    30. *Изменение размера мультика
    31. *Использование векторных линий, и вообще работа с ними
    32. *Рисование каплями (сложно объяснить)
    33. *Конструктор для добавления в стандартные своей фигуры
    34. *Выбор нумерации кадров с 0 или с 1 (по умолчанию с 0)
    35. **Рандомное перемешивание слоёв без возможности это откатить
    36. ***Слияние кадров
    37. ***Преобразование "синего круга" в "красный квадрат"
    38. ***Конструктор персонажа
    39. ***Работа с 3D
    40. *Речевое управление редактором (нарисуй красный круг радиуса 3  центре холста")
    42. ****Нейронки (рисование мультика по введенному тексту)
    43. ****Мультиплеер

