using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class UserSave
{
    public static void SaveUser(User_Manager _myUserManager)
    {
        BinaryFormatter _myFormatter = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "User.GameData");
        FileStream _myStream = new FileStream(path, FileMode.Create);

        UserData _myUserData = new UserData();

        _myFormatter.Serialize(_myStream, _myUserData);
        _myStream.Close();
        Debug.Log("Save Succesfully");
    }

    public static UserData LoadUser()
    {
        string path = Path.Combine(Application.persistentDataPath, "User.GameData");
        if (File.Exists(path))
        {
            BinaryFormatter _myFormatter = new BinaryFormatter();
            FileStream _myStream = new FileStream(path, FileMode.Open);

            UserData _myUserData = _myFormatter.Deserialize(_myStream) as UserData;
            _myStream.Close();
            return _myUserData;
        }
        else
        {
            return null;
        }
    }

}
