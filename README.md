Instructions on how to play:
- Using "WASD" to move the character around
- Collect the gems to unlock the exit (The white square)
- Collect the first two gems to open the door (The yellow sqaure)
- And win

  Report:
  My approach was to only focus on circle and square collision. I tried at first
  to implement a modular approach to the collider as seen with my scripts
  (CollisionManager, SquareManager, CircleManager). But as I continue with
  implementing the script to the player and objects, I continuously found myself running
  into issues. I then just stated to focus on solving the issues and the functions were
  placed in the player movement script. I do believe that I could move them to a different script
  and made it more modular, but for the sake of my own mental health, I leave it as is. Most of
  the calculation made were found in by searching through different pseudo-codes and people's codes.
  
  The issues a ran most into was making sure the player didn't go through walls, which I try to use
  minor setbacks to stop the player going through it. At first I was calculating the setback depeding
  on the object's center. So, if I was neer the center, it would work well, but if it scaled longer and I
  went to the top, it would set me back further. Another issue was that I was able to get the collision point,
  but when I collided, I was being suck into the wall. It was because of this "rb.position -= adjustedPushback".
  I needed to add in order to be properly be pushed back. And then, when I collided agaisnt the top part of the
  square I would go through it as well. So that's why I check the top of the squares.

  To conclude, the "game" os working fine, of course it isn't perfet, but I was able to implement a collision, system,
  add animation, added sound, and added UI. A bit of everything :).
