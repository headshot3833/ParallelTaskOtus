using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace ParallelTaskOtus
{
    public static class Computer
    {
        public static StringBuilder GetParamComputer()
        {
            StringBuilder sb = new StringBuilder();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");

            foreach (ManagementObject obj in searcher.Get())
            {
                sb.Append($"Имя ОС: {obj["Name"]}\n");
                sb.Append($"Версия: {obj["Version"]}\n");
                sb.Append($"Производитель: {obj["Manufacturer"]}\n");
                sb.Append($"Дата установки: {obj["InstallDate"]}\n");
            }
            return sb;
        }
        public static string GetPhysicalMemory(string WIN32_Class)
        {
            string memory = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + WIN32_Class);
            foreach (ManagementObject obj in searcher.Get())
            {
                memory += $"Объем: {Convert.ToUInt64(obj["Capacity"]) / (1024 * 1024)} MB, " +
                                $"Производитель: {obj["Manufacturer"]}, " +
                                $"Модель: {obj["PartNumber"]}\n";
            }
            return memory;
        }
        public static string GerProcessor(string WIN32_Class)
        {
            string processor = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + WIN32_Class);
            foreach (ManagementObject obj in searcher.Get())
            {
                processor += $"Название: {obj["Name"]}\n" +
                         $"Производитель: {obj["Manufacturer"]}\n" +
                         $"Количество ядер: {obj["NumberOfCores"]}\n" +
                         $"Текущая частота: {obj["CurrentClockSpeed"]}MHz\n";
            }
            return processor;
        }
        
    }
}
