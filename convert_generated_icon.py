import os
from PIL import Image

generated_png = r"C:\Users\tomok\.gemini\antigravity-ide\brain\6d821295-6a3b-43fa-9bd7-259b027bbdd3\key_app_icon_1784958821448.png"
workspace_dir = r"c:\Dev\PasswordManager"

img = Image.open(generated_png).convert("RGBA")

# Save lock_icon.png (256x256)
png_path = os.path.join(workspace_dir, "lock_icon.png")
img.resize((256, 256), Image.Resampling.LANCZOS).save(png_path)

# Save icon_32.png
img.resize((32, 32), Image.Resampling.LANCZOS).save(os.path.join(workspace_dir, "icon_32.png"))

# Save lock_icon.ico containing multiple sizes
ico_path = os.path.join(workspace_dir, "lock_icon.ico")
img.save(
    ico_path,
    format="ICO",
    sizes=[(16, 16), (32, 32), (48, 48), (64, 64), (128, 128), (256, 256)]
)

print(f"Successfully converted and updated icons in {workspace_dir}")
