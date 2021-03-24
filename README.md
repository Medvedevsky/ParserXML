# ParserXML

### TryDataParsing - метод позволяющий парсить большие XML файлы, с учетом того, что мы не знаем названия атрибутов. 

Возвращает true - если опирация прошла успешно и false если нет

```c#
public static bool TryDataParsing(string pathToFile, out Dictionary<string, byte> fieldAddresObject,
                                                  out List<List<(byte, string)>> listAddresObjectFIAS)
{
    fieldAddresObject = new Dictionary<string, byte>();
    listAddresObjectFIAS = new List<List<(byte, string)>>();

    int count = 0;

    try
    {
        using (XmlReader reader = XmlReader.Create(new FileStream(pathToFile, FileMode.Open)))
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.HasAttributes)
                {
                    var addressObject = new List<(byte, string)>(fieldAddresObject.Count);

                    while (reader.MoveToNextAttribute())
                    {
                        if (!fieldAddresObject.ContainsKey(reader.Name))
                            fieldAddresObject.Add(reader.Name, (byte)fieldAddresObject.Values.Count);

                        addressObject.Add((fieldAddresObject[reader.Name], reader.Value));
                    }

                    listAddresObjectFIAS.Add(addressObject);

                    count = listAddresObjectFIAS.Count;
                }
            }
            fieldAddresObject = null;
            listAddresObjectFIAS = null;
        }
    }
    catch
    {
        return false;
    }
    Console.WriteLine(count);
    return true;
}
``` 
 
