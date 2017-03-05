# DrawMoar
Raster and vector graphics editor for creating cartoons

**Read it:**
	(!!!) Некомпилирующийся код никогда не должен попадать в репозиторий вообще, даже в ваших ветках. Локально - можно, здесь - нет.
	Создавайте новую ветку от develop и работайте с ней.
    Чтобы из изображений получилось видео они должны быть ОДНОГО размера и ОДНОГО формата (если захотите поиграться с MP4Exporter),
    а их имена должны быть "img\*.png", где * это любой символ или несколько символов (или 0 символов), и они должны находиться в папке
    в Demo/bin/Debug/\*folder_name\*, имя папки указывается в двух методах CreateConcatFile() и Save() в классе Mp4Exporter. Это пока
    так чтобы было легче тестировать эту часть. Потом исправлю когда появится внутреннее представление и сами Frame и Cartoon и Layer,
    можете исправить сами.
    С WPF первый раз, не умею в них, так что особо на мой код не смотрите. Можете смело всё удалять, только примерное расположение
    будущих панелек не нарушайте сильно (типа слои слева, кисти справа).
    
    
**Детали реализации:**
    
        Меню:
        
            Первый пункт меню – “Файл”. Он включает в себя подпункты:
                Новый/Создать/New/Create
                Открыть
                Сохранить
                Сохранить как
                    MP4
                    AVI
                    
            Второй пункт – “Кадр”. Подпункты:
                Добавить (Открыть диалоговое окно с предложением добавить картинку в выбранных нами формата),
                Сохранить как
                    PNG
                    JPEG
                    **PSD
  
  
**Внутреннее представление:**
  
        Что из себя представляет наш собственный формат .drm?
        Это будет ПАПОЧКА (а не zip архив) с файлом манифеста, манифест будет представлять собой JSON файл, со следующей структурой:
        {
	        “Frames”: [
		        {
			        Position: integer,
			        Duration: float,
			        Layers: [
				        {
					        ** “Position”: integer,
					        “Type”: “Vector” / ”Raster”,
					        “Source”: SVG file for “Vector” / Binary file for “Raster” type
				        },
			        ]
		        },
		        {
			        Position: integer,
			        Duration: float,
			        Layers: [
				        {
					        ** “Position”: integer,
					        “Type”: “Vector” / ”Raster”,
					        “Source”: SVG file for “Vector” / Binary file for “Raster” type
				        },
			        ]
		        }
	        ]
        }

![UML](https://github.com/Anonymoar/DrawMoar/blob/master/UML%20Class%20Diagram.jpg)  
    



**Функциональные требования:**

Need it
    
    15. Add to frame standart figure
    13. Export cartoon to *name_of_your_cartoon*.mp4
    12. Export frame to *name_of_your_image*.PNG
    
It works
    
    15. Add to frame standart figure +- [only line// todo: click on canvas]
    13. Export images to *name_of_your_cartoon*.mp4 [todo: (maybe) delete video name from video]
    12. Export canvas to *name_of_your_image*.PNG
   
Coming soon

    2. Create new empty frame
    4. Delete frame
    1. Create new cartoon (const size)
    5. Reorder frames
    6. Create new empty layer
    8. Delete layer
    8.1 Reorder layers
    17. Use brush
    
I'm going to do it and you will be able to see it
  
    1. Create new cartoon (const size)
    2. Create new empty frame
    3. Create a copy of frame
    4. Delete frame
    5. Reorder frames
  
    6. Create new empty layer
    7. Create a copy of layer
    8. Delete layer
    8.1 Reorder layers
    9. Convert vector layer to raster layer
    10. Merge layers (R + R = R; V + R = R; V + V = V)
  
    11. Save intermediate result in *name_of_your_cartoon*.drm
    12. Export frame to *name_of_your_image*.PNG
    13. Export cartoon to *name_of_your_cartoon*.mp4
  
    14. Add to frame another image
    15. Add to frame standart figure
    16. Translate, rotate, scale (on layer)
    17. Use brush
    18. Get pixel color
    19. Use eraser
  
    20. Select a fragment of layer
    21. Translate, rotate, scale (on fragment of layer)
    22. Cut a fragment of layer
    23. Use paint bucket tool
  
    24. Undo recent changes
    25. Text
    26. Use textures as in Photoshop for vectorLayers
    27. "clouds" for dialogs and dreams (change size under text)
    28. Use effects and filters as in Photoshop for frames and layers
  
Maybe I'll do it and you probably will be able to see it

    29. *Interface language selection (Russian, English, German)
    30. *Change the image size
    31. *Use vector lines
    32. *Use the drops for drawing
    33. *Create new figure (figure constructor)
    34. *Choose 0 or 1 for first frame
    35. **Random mix frames
    36. ***Merge frames
    37. ***Convert "blue circle" to "red square"
    38. ***Character constructor
