import time
import subprocess
from watchdog.observers import Observer
from watchdog.events import FileSystemEventHandler

class FileChangeHandler(FileSystemEventHandler):
    def on_modified(self, event):
        if event.src_path == file_path:
            print(f"Файл '{file_path}' был изменен. Запуск 'dotnet build'...")
            result = subprocess.run(["dotnet", "build"], capture_output=True, text=True)
            
            print(result.stdout)
            if result.returncode != 0:
                print("Произошла ошибка при выполнении 'dotnet build':")
                print(result.stderr)

file_path = "C:\\coding\\Game1.cs"
event_handler = FileChangeHandler()
observer = Observer()
observer.schedule(event_handler, path=file_path, recursive=True)

print(f"Отслеживание изменений в файле: {file_path}")
observer.start()

try:
    while True:
        time.sleep(1)
except KeyboardInterrupt:
    observer.stop()

observer.join() 