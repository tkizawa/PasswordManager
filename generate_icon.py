#!/usr/bin/env python3
"""
Generate a lock/key icon for the PasswordManagerApp
"""

from PIL import Image, ImageDraw

def create_lock_icon(size=256, filename='icon.ico'):
    """Create a lock icon with a modern design"""
    
    # Create image with transparent background
    img = Image.new('RGBA', (size, size), color=(0, 0, 0, 0))
    draw = ImageDraw.Draw(img)
    
    # Color: Blue-ish security color
    lock_color = (41, 128, 185)  # Nice blue
    highlight_color = (52, 152, 219)  # Lighter blue
    
    # Draw lock body (rounded rectangle)
    body_left = size // 4
    body_top = size // 3
    body_right = size * 3 // 4
    body_bottom = size * 4 // 5
    
    # Draw rounded rectangle for lock body
    draw.rectangle(
        [body_left, body_top, body_right, body_bottom],
        fill=lock_color,
        outline=lock_color,
        width=2
    )
    
    # Draw shackle (lock top arc)
    shackle_left = size // 3
    shackle_top = size // 8
    shackle_right = size * 2 // 3
    shackle_bottom = size // 2
    
    # Draw arc for shackle
    draw.arc(
        [shackle_left, shackle_top, shackle_right, shackle_bottom],
        start=0,
        end=180,
        fill=lock_color,
        width=int(size // 20)
    )
    
    # Draw keyhole
    keyhole_x = size // 2
    keyhole_y = size // 2 + size // 12
    keyhole_radius = size // 16
    
    # Keyhole circle
    draw.ellipse(
        [
            keyhole_x - keyhole_radius,
            keyhole_y - keyhole_radius,
            keyhole_x + keyhole_radius,
            keyhole_y + keyhole_radius
        ],
        fill=(0, 0, 0, 100),  # Semi-transparent black for depth
        outline=highlight_color,
        width=1
    )
    
    # Save as ICO and PNG
    # For ICO, we need different size variants
    sizes = [16, 32, 48, 64, 128, 256]
    icons = []
    
    for s in sizes:
        resized = img.resize((s, s), Image.Resampling.LANCZOS)
        icons.append(resized)
    
    # Save as ICO with multiple sizes
    icons[0].save(
        filename,
        format='ICO',
        sizes=[(s, s) for s in sizes]
    )
    
    print(f"✓ Icon created: {filename}")
    
    # Also save as PNG for preview
    img.save('icon.png')
    print(f"✓ Preview saved: icon.png")

if __name__ == '__main__':
    create_lock_icon(size=256, filename='lock_icon.ico')
