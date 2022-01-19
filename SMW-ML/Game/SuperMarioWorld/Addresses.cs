﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMW_ML.Game.SuperMarioWorld
{
    internal static class Addresses
    {
        public struct AddressData
        {
            public AddressData(uint address, uint length)
            {
                Address = address;
                Length = length;
            }

            public uint Address;
            public uint Length;
        }


        public static class Player
        {
            /// <summary>
            /// Horizontal position of the player, in pixels
            /// </summary>
            public static readonly AddressData PositionX = new(0x000094, 2);
            /// <summary>
            /// Vertical position of the player, in pixels
            /// </summary>
            public static readonly AddressData PositionY = new(0x000096, 2);
            /// <summary>
            /// 0b:00 when not on ground, 0b:01 when on layer 1, 0b:10 when on layer 2.
            /// Value is doubled when running fast enough up a slope
            /// </summary>
            public static readonly AddressData IsOnGround = new(0x0013EF, 1);
            /// <summary>
            /// 0x00 = if not standing on Solid Sprite
            /// 0x01 = Standing on top of a floating rock, floating grass platform, floating skull, Mega Mole, carrot top lift, etc.
            /// 0x02 = Standing on top of a springboard, pea bouncer.
            /// 0x03 = Standing on top of a brown chained platform, gray falling platform.
            /// </summary>
            public static readonly AddressData IsOnSolidSprite = new(0x001471, 1);
            /// <summary>
            /// Notable values:
            /// 0x00 = Player on ground
            /// 0x0B = Jumping/swimming upwards.
            /// 0x0C = Shooting out of a slanted pipe, running at maximum speed.
            /// 0x24 = Descending/sinking.
            /// </summary>
            public static readonly AddressData AirFlag = new(0x000072, 1);
            /// <summary>
            /// 0x01 when in water, 0x00 otherwise
            /// </summary>
            public static readonly AddressData IsInWater = new(0x000075, 1);
            /// <summary>
            /// Is carrying something if 0x01, 0x00 otherwise
            /// </summary>
            public static readonly AddressData IsCarryingSomething = new(0x00148F, 1);
            /// <summary>
            /// 0x01 when the player can climb in the air (Used for the ropes), 0x00 otherwise
            /// </summary>
            public static readonly AddressData CanClimbOnAir = new(0x0018BE, 1);
            /// <summary>
            /// 0x01 if inside a lakitu cloud, 0x00 otherwise
            /// </summary>
            public static readonly AddressData IsInLakituCloud = new(0x0018C2, 1);

            /// <summary>
            /// 00	None: the player is able to move freely.
            /// 01	Flashing as if the player is hurt by an enemy.
            /// 02	Get Mushroom animation.
            /// 03	Get Feather animation. (Note: to make it work, write to $1496 also.)
            /// 04	Get Fire Flower animation. (Note: to make it work, write to $149B also.)
            /// 05	Enter a horizontal Warp Pipe.
            /// 06	Enter a vertical Warp Pipe.
            /// 07	Shoot from a slanted pipe.
            /// 08	Shoot up into the sky. (Yoshi Wings)
            /// 09	End level without activating overworld events. (Dying)
            /// 0A Castle entrance moves.
            /// 0B	Freeze player (used during the bowser defeated cutscene, and also disables HDMA).
            /// 0C Castle destruction moves.
            /// 0D	Enter a door.
            /// </summary>
            public static readonly AddressData PlayerAnimationState = new(0x000071, 1);

            public static class PlayerAnimationStates
            {
                public const byte NONE = 0x00;
                public const byte FLASHING = 0x01;
                public const byte GET_MUSHROOM = 0x02;
                public const byte GET_FEATHER = 0x03;
                public const byte GET_FIRE_FLOWER = 0x04;
                public const byte ENTER_HORIZONTAL_PIPE = 0x05;
                public const byte ENTER_VERTICAL_PIPE = 0x06;
                public const byte SHOOT_FROM_PIPE = 0x07;
                public const byte YOSHI_WINGS = 0x08;
                public const byte DYING = 0x09;
                public const byte CASTLE_ENTRANCE_MOVE = 0x0A;
                public const byte PLAYER_FROZEN = 0x0B;
                public const byte CASTLE_DESTRUCTIONS_MOVE = 0x0C;
                public const byte ENTER_DOOR = 0x0D;
            }

            /// <summary>
            /// Counts down while the player is invulnerable  (due to taking damage)
            /// </summary>
            public static readonly AddressData FlashingTimer = new(0x001497, 1);
            /// <summary>
            /// Which item the player has in its item box
            /// 0x00 = None; 0x01 = Mushroom; 0x02 = Fire Flower; 0x03 = Star; 0x04 = Feather.
            /// </summary>
            public static readonly AddressData ItemBox = new(0x000DC2, 1);
            /// <summary>
            /// Set if the player is wall running, 0x00 otherwise
            /// </summary>
            public static readonly AddressData IsWallRunning = new(0x0013E3, 1);
            /// <summary>
            /// Increments by 0x02 every frame when the player is running with the Dash button pressed. 0x70 means the player is at max speed, and can fly
            /// </summary>
            public static readonly AddressData DashTimer = new(0x0013E4, 1);
            /// <summary>
            /// 0x01 if the player can jump out of the water immediately, 0x00 otherwise
            /// </summary>
            public static readonly AddressData CanJumpOutOfWater = new(0x0013FA, 1);
        }

        public static class Sprite
        {
            /// <summary>
            /// Table of Sprite IDs. See <see href="https://docs.google.com/spreadsheets/d/1YuEyTkBXl-BvXyAf6C7EPXo20CdVlbwUttp2RXHoY2U/edit#gid=0"/> for more information
            /// </summary>
            public static readonly AddressData SpriteNumbers = new(0x00009E, 12);
            /// <summary>
            /// Sprite statuses
            /// Anything equal or above 0x08 is alive
            /// <br>0x00	Free slot, non-existent sprite.</br>
            /// <br>0x01	Initial phase of sprite.</br>
            /// <br>0x02	Killed, falling off screen.</br>
            /// <br>0x03	Smushed. Rex and shell-less Koopas can be in this state.</br>
            /// <br>0x04	Killed with a spinjump.</br>
            /// <br>0x05	Burning in lava; sinking in mud.</br>
            /// <br>0x06	Turn into coin at level end.</br>
            /// <br>0x07	Stay in Yoshi's mouth.</br>
            /// <br>0x08	Normal routine.</br>
            /// <br>0x09	Stationary / Carryable.</br>
            /// <br>0x0A	Kicked.</br>
            /// <br>0x0B	Carried.</br>
            /// <br>0x0C	Powerup from being carried past goaltape.</br>
            /// </summary>
            public static readonly AddressData Statuses = new(0x0014C8, 12);
            /// <summary>
            /// Sprite X Speeds
            /// </summary>
            public static readonly AddressData XSpeeds = new(0x0000B6, 12);
            /// <summary>
            /// Sprite Y Speeds
            /// </summary>
            public static readonly AddressData YSpeeds = new(0x0000AA, 12);

            /// <summary>
            /// Low byte of the 16-bit sprite X positions
            /// </summary>
            public static readonly AddressData LowByteXPositions = new(0x0000D8, 12);
            /// <summary>
            /// High byte of the 16-bit sprite X positions
            /// </summary>
            public static readonly AddressData HighByteXPositions = new(0x0014E0, 12);
            /// <summary>
            /// Low byte of the 16-bit sprite Y positions
            /// </summary>
            public static readonly AddressData LowByteYPositions = new(0x0000E4, 12);
            /// <summary>
            /// High byte of the 16-bit sprite Y positions
            /// </summary>
            public static readonly AddressData HighByteYPositions = new(0x0014D4, 12);
            /// <summary>
            /// 0x01 if the sprite is on Yoshi's tongue
            /// </summary>
            public static readonly AddressData SpritesAreOnYoshiTongue = new(0x0015D0, 12);
            /// <summary>
            /// <br>Format: sSjJcccc</br>
            /// <br>s=Disappear in cloud of smoke.</br>
            /// <br>S=Hop in/kick shells.</br>
            /// <br>j=Dies when jumped on.</br>
            /// <br>J=Can be jumped on (false = player gets hurt if they jump on the sprite, but can bounce off with a spin jump).</br>
            /// <br>cccc=Object clipping.</br>
            /// </summary>
            public static readonly AddressData SpritesProperties1 = new(0x001656, 12);
            /// <summary>
            /// <br>Format: dscccccc</br>
            /// <br>d=Falls straight down when killed</br>
            /// <br>s=Use shell as death frame</br>
            /// <br>cccccc=Sprite clipping</br>
            /// </summary>
            public static readonly AddressData SpritesProperties2 = new(0x001662, 12);
            /// <summary>
            /// <br>Format: lwcfpppg</br>
            /// <br>l=Don't interact with layer 2 (or layer 3 tides)</br>
            /// <br>w=Disable water splash</br>
            /// <br>c=Disable cape killing</br>
            /// <br>f=Disable fireball killing</br>
            /// <br>ppp=Palette</br>
            /// <br>g=Use second graphics page</br>
            /// </summary>
            public static readonly AddressData SpritesProperties3 = new(0x00166E, 12);
            /// <summary>
            /// <br>Format: dpmksPiS</br>
            /// <br>d=Don't use default interaction with player</br>
            /// <br>p=Gives power-up when eaten by Yoshi</br>
            /// <br>m=Process interaction with player every frame</br>
            /// <br>k=Can't be kicked like a shell</br>
            /// <br>s=Don't change into a shell when stunned</br>
            /// <br>P=Process while off screen</br>
            /// <br>i=Invincible to star/cape/fire/bouncing bricks</br>
            /// <br>S=Don't disable clipping when killed with star</br>
            /// </summary>
            public static readonly AddressData SpritesProperties4 = new(0x00167A, 12);
            /// <summary>
            /// <br>Format: dnctswye</br>
            /// <br>d=Don't interact with objects</br>
            /// <br>n=Spawns a new sprite</br>
            /// <br>c=Don't turn into a coin when goal passed</br>
            /// <br>t=Don't change direction if touched</br>
            /// <br>s=Don't interact with other sprites</br>
            /// <br>w=Weird ground behavior</br>
            /// <br>y=Stay in Yoshi's mouth</br>
            /// <br>e=Inedible</br>
            /// </summary>
            public static readonly AddressData SpritesProperties5 = new(0x001686, 12);
            /// <summary>
            /// <br>Format: wcdj5sDp</br>
            /// <br>w=Don't get stuck in walls (carryable sprites)</br>
            /// <br>c=Don't turn into a coin with silver POW</br>
            /// <br>d=Death frame 2 tiles high</br>
            /// <br>j=Can be jumped on with upward Y speed</br>
            /// <br>5=Takes 5 fireballs to kill. Clear means it's killed by one. The hit counter is at $7E:1528.</br>
            /// <br>s=Can't be killed by sliding</br>
            /// <br>D=Don't erase when goal passed</br>
            /// <br>p=Make platform passable from below</br>
            /// </summary>
            public static readonly AddressData SpritesProperties6 = new(0x00190F, 12);

            /// <summary>
            /// 0x00 : No shooter
            /// 0x01 : Bullet bill shooter
            /// 0x02 : Torpedo bill shooter
            /// </summary>
            public static readonly AddressData ShooterNumbers = new(0x001783, 8);
            /// <summary>
            /// Low byte of the 16-bit shooter X positions
            /// </summary>
            public static readonly AddressData LowByteShooterXPositions = new(0x00179B, 8);
            /// <summary>
            /// High byte of the 16-bit shooter X positions
            /// </summary>
            public static readonly AddressData HighByteShooterXPositions = new(0x0017A3, 8);
            /// <summary>
            /// Low byte of the 16-bit shooter Y positions
            /// </summary>
            public static readonly AddressData LowByteShooterYPositions = new(0x00178B, 8);
            /// <summary>
            /// High byte of the 16-bit shooter Y positions
            /// </summary>
            public static readonly AddressData HighByteShooterYPositions = new(0x001793, 8);

        }

        public static class Yoshi
        {
            /// <summary>
            /// 0x01 or 0x02 if riding Yoshi
            /// </summary>
            public static readonly AddressData IsRidingYoshi = new(0x00187A, 1);
            /// <summary>
            /// Yoshi color. 0x04=yellow; 0x06=blue; 0x08=red; 0x0A=green
            /// </summary>
            public static readonly AddressData YoshiColor = new(0x0013C7, 1);
            /// <summary>
            /// Set to 0x02 if Yoshi has wings. 0x00 otherwise
            /// </summary>
            public static readonly AddressData YoshiHasWings = new(0x00141E, 1);
            /// <summary>
            /// Counts down every 4 frames until Yoshi swallows what it has in its mouth
            /// </summary>
            public static readonly AddressData YoshiSwallowsTimer = new(0x0018AC, 1);
            /// <summary>
            /// Yoshi's slot in the sprites table. Set to 0 if Yoshi doesn't exist
            /// </summary>
            public static readonly AddressData YoshiSpriteSlot = new(0x0018E2, 1);
            /// <summary>
            /// Set to 0x01 when Yoshi can stomp (when it has a yellow shell in its mouth, or when it has any shell as a yellow yoshi). 
            /// 0x00 otherwise.
            /// </summary>
            public static readonly AddressData YoshiCanStomp = new(0x0018E7, 1);
            /// <summary>
            /// Whether or not Yoshi has a key in its mouth.
            /// </summary>
            public static readonly AddressData HasKeyInMouth = new(0x00191C, 1);
        }

        public static class Timer
        {
            /// <summary>
            /// Counts down every 4 frames as the blue switch timer runs out
            /// </summary>
            public static readonly AddressData BlueSwitchTimer = new(0x0014AD, 1);
            /// <summary>
            /// Counts down every 4 frames as the silver switch timer runs out
            /// </summary>
            public static readonly AddressData SilverSwitchTimer = new(0x0014AE, 1);
            /// <summary>
            /// Counts down every 4 frames as the P Balloon runs out.
            /// </summary>
            public static readonly AddressData PBalloonTimer = new(0x001891, 1);
            /// <summary>
            /// Counts down every 4 frames, time before lakitu cloud disappears
            /// </summary>
            public static readonly AddressData LakituCloudTimer = new(0x0018E0, 1);
        }

        public static class Level
        {
            /// <summary>
            /// 0x01 when in a water level, 0x00 otherwise
            /// </summary>
            public static readonly AddressData IsWater = new(0x000085, 1);
            /// <summary>
            /// 0x00 if not slippery, 0x01 through 0x7F if half-slippery, 0x80 to 0xFF for full slippery
            /// </summary>
            public static readonly AddressData IsSlippery = new(0x000085, 1);
            /// <summary>
            /// 0x01 if the level is paused, 0x00 otherwise
            /// </summary>
            public static readonly AddressData IsPaused = new(0x0013D4, 1);
            /// <summary>
            /// Timer for the level. 
            /// 0x30 is the frame counter (Starts at 0x28 and counts down every frame). Once it reaches 0xFF (negative), 1 second is subtracted from the timer seconds and it is reset to 0x28. This means the timer counts down a second every 41 frames.
            /// 0x31 is the timer's Hundred seconds digit
            /// 0x32 is the timer's Tenths seconds digit
            /// 0x33 is the timer's Single seconds digit
            /// </summary>
            public static readonly AddressData Timer = new(0x000F30, 4);
            /// <summary>
            /// Level is over when set to non-zero value.
            /// </summary>
            public static readonly AddressData EndLevelTimer = new(0x001493, 1);
            /// <summary>
            /// Which event to trigger on the overworld and how the level was exited
            /// 00	No event; do nothing
            /// 01	Normal exit
            /// 02	Secret exit 1
            /// 03	Secret exit 2
            /// 04	Secret exit 3
            /// 80	No event; level exited with start+select or dying (or "switch players" secondary exit action)
            /// E0  No event, but show save prompt anyway (used if the level was beaten a second time and its tile corresponds to one of the tiles at $04E5E6)
            /// </summary>
            public static readonly AddressData LevelExitedEvent = new(0x000DD5, 1);
            /// <summary>
            /// Set to 0x30 when keyhole is triggered
            /// </summary>
            public static readonly AddressData KeyholeTimer = new(0x001434, 1);
            /// <summary>
            /// Screen mode: CD----Vv.
            /// C = Collision with layer 2.
            /// D = Disable collision with layer 1.
            /// V = Vertical layer 2.
            /// v = Vertical layer 1.
            /// - = unused.
            /// </summary>
            public static readonly AddressData ScreenMode = new(0x00005B, 1);
            /// <summary>
            /// Amount of screens in level. Set to FF in Ludwig & Reznor fights
            /// </summary>
            public static readonly AddressData ScreenCount = new(0x00005D, 1);

            /// <summary>
            /// 0x00 : there is no water tide in the level
            /// 0x01 : the tide has a variable height
            /// 0x02 : the tide has a fixed height
            /// </summary>
            public static readonly AddressData WaterTide = new(0x001403, 1);
            /// <summary>
            /// 0x00 : No scroll
            /// 0x01 : Fixed speed scroll
            /// 0x02 : Variable speed scroll
            /// </summary>
            public static readonly AddressData HorizontalScrollLayer2 = new(0x001413, 1);
            /// <summary>
            /// val type            speed  
            /// <br> 00	None	    0        </br>
            /// <br> 01	Constant	1        </br>
            /// <br> 02	Variable	1/2      </br>
            /// <br> 03	Slow	    1/32     </br> 
            /// </summary>
            public static readonly AddressData VerticalScrollLayer2 = new(0x001413, 1);
            /// <summary>
            /// 0x00 = none; 0x01 = message 1; 0x02 = message 2; 0x03 = Yoshi thanks message.
            /// </summary>
            public static readonly AddressData TextBoxTriggered = new(0x001426, 1);
            /// <summary>
            /// Scroll command number
            /// <br>00	None</br>
            /// <br>01	Auto-scroll 1, 2, 3, 4 (Sprite E8)</br>
            /// <br>02	Layer 2 smash 1, 2, 3 (Sprite E9)</br>
            /// <br>03	Layer 2 scroll 1, 2, 3, 4 (Sprite EA)</br>
            /// <br>04	Unused scroll command (Sprite EB)</br>
            /// <br>05	Unused scroll command (Sprite EC)</br>
            /// <br>06	Layer 2 falls (Sprite ED)</br>
            /// <br>07	Unused scroll command (Sprite EE)</br>
            /// <br>08	Layer 2 scroll S/L (Sprite EF)</br>
            /// <br>09	Unused scroll command (Sprite F0)</br>
            /// <br>0A	Unused scroll command (Sprite F1)</br>
            /// <br>0B	Layer 2 On/Off switch controlled (Sprite F2)</br>
            /// <br>0C	Auto-scroll level (Sprite F3)</br>
            /// <br>0D	Fast background scroll (Sprite F4)</br>
            /// <br>0E	Layer 2 sink/rise when touched (Sprite F5)</br>
            /// </summary>
            public static readonly AddressData ScrollCommand = new(0x00143E, 1);
            /// <summary>
            /// See <see cref="ScrollCommand"/> for command numbers
            /// </summary>
            public static readonly AddressData ScrollCommandLayer2 = new(0x00143F, 1);

            /// <summary>
            /// <br>Map16 Low Byte Table.</br>
            /// <br>For horizontal levels, $1B0 tiles per screen, where each screen can be indexed using the format ------y yyyyxxxx. $7E:FE00-$7E:FFFF are unused.</br>
            /// <br>For vertical levels, $200 bytes per screen, with the format --sssssx yyyyxxxx. All bytes are used.</br>
            /// <br>If layer 2 or 3 is interactive in the level, it uses screens 10-1F (0E-1B in vertical levels).</br>
            /// </summary>
            public static readonly AddressData Map16LowBytes = new(0x00C800, 14_336);
            /// <summary>
            /// Map 16 High Byte Table.
            /// Completes <see cref="Map16LowBytes"/>.
            /// </summary>
            public static readonly AddressData Map16HighBytes = new(0x01C800, 14_336);

        }

        public static class Counters
        {
            /// <summary>
            /// 24-bit Little endian score. Note that the score stored is 10x lower than the displayed score.
            /// </summary>
            public static readonly AddressData Score = new(0x000F34, 3);
            /// <summary>
            /// Player lives count, minus 1 (0x04 is 5 lives)
            /// </summary>
            public static readonly AddressData Lives = new(0x000DBE, 1);
            /// <summary>
            /// Player coins count
            /// </summary>
            public static readonly AddressData Coins = new(0x000DBF, 1);
            /// <summary>
            /// Increments every time a level transition occurs. Starts at 0 on level load. Can be used to determine when entering sub areas
            /// </summary>
            public static readonly AddressData LevelTransitionCounter = new(0x00141A, 1);
            /// <summary>
            /// The amount of yoshi coins collected
            /// </summary>
            public static readonly AddressData YoshiCoinCollected = new(0x001420, 1);

        }
    }
}
