using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class UploadPatient : MonoBehaviour
{
    [Header("UI Elements")]
    public InputField nameInputField;
    public InputField ageInputField;
    public Dropdown genderDropdown;
    public Button submitButton;
    public Transform patientsListContent; // Reference to the Scroll View's Content
    public GameObject patientEntryPrefab;  // Reference to the PatientEntry Prefab

    private List<Patient> patients = new List<Patient>();

    private void Start()
    {
        submitButton.onClick.AddListener(OnSubmitButtonClicked);
        patients = LoadPatientsFromFile();
         foreach (var patient in patients)
    {
        AddPatientToUI(patient);
    }
    }

    private void OnSubmitButtonClicked()
    {
        string selectedGender = genderDropdown.options[genderDropdown.value].text;

        Patient patient = new Patient
        {
            name = nameInputField.text,
            age = int.Parse(ageInputField.text),
            gender = selectedGender
        };

        patients.Add(patient);
        AddPatientToUI(patient);
        SavePatientsToFile();
    }

    private void AddPatientToUI(Patient patient)
    {
        GameObject newEntry = Instantiate(patientEntryPrefab, patientsListContent);
        newEntry.transform.Find("patientInfo").GetComponent<Text>().text = patient.ToString();
        Button deleteButton = newEntry.transform.Find("deletePatient").GetComponent<Button>();
        deleteButton.onClick.AddListener(() => 
        {
            patients.Remove(patient);
            Destroy(newEntry);
            SavePatientsToFile();
            Debug.Log(Application.persistentDataPath);
        });
    }

    public void SavePatientsToFile()
    {
        string json = JsonUtility.ToJson(new SerializableList(patients), prettyPrint: true);
        File.WriteAllText(GetFilePath(), json);
    }

    public List<Patient> LoadPatientsFromFile()
    {
        if (File.Exists(GetFilePath()))
        {
            string json = File.ReadAllText(GetFilePath());
            SerializableList loadedData = JsonUtility.FromJson<SerializableList>(json);
            return loadedData.patients;
        }
        return new List<Patient>();
    }

    private string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, "patients.json");
    }

    [System.Serializable]
    public class Patient
    {
        public string name;
        public int age;
        public string gender;

        public override string ToString()
        {
            return $"Patient: {name}, Age: {age}, Gender: {gender}";
        }
    }

    [System.Serializable]
    private class SerializableList
    {
        public List<Patient> patients;

        public SerializableList(List<Patient> patients)
        {
            this.patients = patients;
        }
    }
}


