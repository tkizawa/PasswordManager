#!/usr/bin/env python3
"""
Generate a lock/key icon for the PasswordManagerApp
"""

from PIL import Image, ImageDraw

def create_lock_icon(size=256, filename='lock_icon.png'):
    """Create a professional lock icon with modern design"""
    
    # Create image with solid background
    img = Image.new('RGBA', (size, size), color=(255, 255, 255, 255))
    draw = ImageDraw.Draw(img)
    
    # Colors
    lock_color = (0, 102, 204)  # Deep blue
    shackle_color = (0, 153, 255)  # Bright blue
    keyhole_color = (80, 80, 80)  # Dark gray
    
    stroke_width = max(2, size // 64)
    
    # Draw shackle (top arc)
    shackle_left = size // 5
    shackle_top = size // 6
    shackle_right = size * 4 // 5
    shackle_bottom = size // 2
    
    draw.arc(
        [shackle_left, shackle_top, shackle_right, shackle_bottom],
        start=0,
        end=180,
        fill=shackle_color,
        width=int(size // 12)
    )
    
    # Draw lock body (main rectangle)
    body_left = size // 5
    body_top = size * 2 // 5
    body_right = size * 4 // 5
    body_bottom = size * 4 // 5
    
    # Fill lock body
    draw.rectangle(
        [body_left, body_top, body_right, body_bottom],
        fill=lock_color,
        outline=lock_color
    )
    
    # Draw border for lock body
    draw.rectangle(
        [body_left, body_top, body_right, body_bottom],
        outline=shackle_color,
        width=stroke_width
    )
    
    # Draw keyhole
    keyhole_x = size // 2
    keyhole_y = size * 3 // 5
    keyhole_radius = size // 14
    
    # Keyhole outer circle
    draw.ellipse(
        [
            keyhole_x - keyhole_radius,
            keyhole_y - keyhole_radius,
            keyhole_x + keyhole_radius,
            keyhole_y + keyhole_radius
        ],
        fill=keyhole_color,
        outline=keyhole_color
    )
    
    # Keyhole inner circle (lighter for depth)
    inner_radius = keyhole_radius // 2
    draw.ellipse(
        [
            keyhole_x - inner_radius,
            keyhole_y - inner_radius,
            keyhole_x + inner_radius,
            keyhole_y + inner_radius
        ],
        fill=(100, 100, 100),
        outline=(100, 100, 100)
    )
    
    # Save as PNG
    img.save(filename)
    print(f"✓ Icon created: {filename}")
    
    # Also save 32x32 version (Windows default)
    img_32 = img.resize((32, 32), Image.Resampling.LANCZOS)
    img_32.save('icon_32.png')
    print(f"✓ 32x32 preview saved: icon_32.png")
    
    import os
    if os.path.exists(filename):
        file_size = os.path.getsize(filename)
        print(f"✓ File size: {file_size} bytes")

if __name__ == '__main__':
    create_lock_icon(size=256, filename='lock_icon.png')
