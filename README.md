Title: Custom Template Editor for Unity

Description:
This repository contains a customizable template editor tool for Unity, designed to streamline the creation and management of custom templates within Unity projects. The Custom Template Editor provides a user-friendly interface for creating, editing, and exporting template data in JSON format, allowing for easy integration into Unity scenes.

Features:

Custom Template Data Management:

Create and manage custom template data with customizable properties such as object names, positions, rotations, scales, and colors.
Organize template data into hierarchical structures with support for nested objects.
JSON Serialization:

Serialize template data to JSON format for easy storage and transferability.
Import template data from JSON files to quickly populate the editor with pre-defined templates.
Visual Editor Interface:

Intuitive editor interface integrated directly into the Unity Editor window for seamless workflow.
Interactive GUI elements for adding, modifying, and saving template data.
Prefab Instantiation:

Instantiate template objects directly into Unity scenes with a single click.
Support for instantiating prefabs with customizable properties, including position, rotation, scale, and color.
Usage Instructions:

Setup:

Clone or download the repository and add the Custom Template Editor folder to your Unity project's Assets directory.
Ensure that the necessary dependencies are present, including the Unity Editor and appropriate Unity modules.
Accessing the Editor:

Open the Unity Editor and navigate to the 'Window' menu.
Select 'Custom Template Editor' to open the Custom Template Editor window.
Creating Custom Templates:

Click the '+' button to create a new custom template.
Enter a name for the template and customize its properties using the provided fields.
Add objects to the template by specifying their names, positions, rotations, scales, colors, and child objects as necessary.
Saving Templates:

To save the template as a JSON file, click the 'Save as JSON template' button.
Choose a destination folder within your Unity project to save the JSON file.
Loading Templates:

Load existing template data from JSON files by clicking the 'Load JSON' button.
Select the desired JSON file containing the template data to import it into the editor.
Instantiating Templates:

To instantiate template objects into your Unity scene, click the 'Instantiate Image' button.
Objects will be instantiated under the specified parent transform, following the hierarchical structure defined in the template.
