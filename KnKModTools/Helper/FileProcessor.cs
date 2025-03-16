namespace KnKModTools.Helper
{
    public class FileProcessor
    {
        private int _totalFiles;
        private int _processedFiles;
        private readonly object _progressLock = new object();
        private readonly int _maxThreadCount;

        public event Action OnAllFilesProcessed;

        public event Action<double> OnProgressChanged;

        public FileProcessor(int maxThreadCount)
        {
            _maxThreadCount = maxThreadCount;
        }

        public async Task ProcessFilesAsync(List<string> files, Action<string> processFileAction)
        {
            _totalFiles = files.Count;
            _processedFiles = 0;

            var tasks = new List<Task>();
            var semaphore = new SemaphoreSlim(_maxThreadCount);

            foreach (var file in files)
            {
                await semaphore.WaitAsync();
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        processFileAction(file);
                    }
                    finally
                    {
                        semaphore.Release();
                        UpdateProgress();
                    }
                }));
            }

            await Task.WhenAll(tasks);
            OnAllFilesProcessed?.Invoke();
        }

        private void UpdateProgress()
        {
            lock (_progressLock)
            {
                _processedFiles++;
                if (_processedFiles % 3 == 0 || _processedFiles == _totalFiles)
                {
                    var progress = (double)_processedFiles / _totalFiles * 100;
                    OnProgressChanged?.Invoke(progress);
                }
            }
        }
    }
}