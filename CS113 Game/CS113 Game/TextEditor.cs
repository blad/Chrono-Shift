using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CS113_Game
{
    public class TextEditor
    {
        private ArrayList word_List;

        public TextEditor()
        {
            word_List = new ArrayList();
        }

        public Word word(String s, Vector2 position, float scale)
        {
           Word word = new Word(s, position, scale);
           word_List.Add(word);
           return word;
        }

        public Word updateWord(Word word, string newWord)
        {
            Word wordToAdd = null;

            foreach (Word w in word_List)
            {
                if (w.Equals(word))
                {
                    wordToAdd = new Word(newWord, w.position, w.scale);  
                }
            }

            if (wordToAdd != null)
            {
                word_List.Add(wordToAdd);
                word_List.Remove(word);
            }

            return wordToAdd;
        }

        public void translateWord(Word word, int x, int y)
        {
            word.translateWord(x, y);
            word.position.X += x;
            word.position.Y += y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Word W in word_List)
                W.Draw(spriteBatch);
        }

        public class Word
        {
            public string word;
            public Vector2 position;
            public float scale;

            Letter[] letters;
            

            public Word(String word, Vector2 position, float scale)
            {
                this.word = word;
                this.position = position;
                this.scale = scale;
                letters = new Letter[word.Length];

                for (int i = 0; i < letters.Length; i++)
                {
                    Vector2 letterPosition = new Vector2(position.X, position.Y);
                    letters[i] = new Letter(word[i], letterPosition, i, scale);
                }
            }

            public void translateWord(int x, int y)
            {
                foreach (Letter L in letters)
                {
                    
                    L.translateLetter(x, y);
                }
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                foreach (Letter L in letters)
                {
                    L.Draw(spriteBatch);
                }
            }
        }

        public class Letter
        {
            private Texture2D HUD_Items;
            private Rectangle location_Rect;
            private Rectangle screen_Rect;
            private char character;
            private int letter_Width = 36;
            private int letter_Height = 50;
            private int x_Offset = 145;
            private int y_Offset = 135;
            private int letter_X_Offset;
            private int letter_Y_Offset;

            public Letter(char character, Vector2 position, int letter_Offset, float scale)
            {
                HUD_Items = Game1.content_Manager.Load<Texture2D>("Backgrounds/HUD/HUD_Items");

                this.character = character;
                screen_Rect = new Rectangle((int)position.X + (int)(scale* (letter_Width*letter_Offset)), (int)position.Y, (int)(scale * letter_Width), (int)(scale * letter_Height));

                switch (character)
                {
                    // a lot of cases....
                    case ('0'): letter_X_Offset = 0; letter_Y_Offset = 0;
                        break;

                    case ('1'): letter_X_Offset = 1; letter_Y_Offset = 0;
                        break;

                    case ('2'): letter_X_Offset = 2; letter_Y_Offset = 0;
                        break;

                    case ('3'): letter_X_Offset = 3; letter_Y_Offset = 0;
                        break;

                    case ('4'): letter_X_Offset = 4; letter_Y_Offset = 0;
                        break;

                    case ('5'): letter_X_Offset = 5; letter_Y_Offset = 0;
                        break;

                    case ('6'): letter_X_Offset = 6; letter_Y_Offset = 0;
                        break;

                    case ('7'): letter_X_Offset = 7; letter_Y_Offset = 0;
                        break;

                    case ('8'): letter_X_Offset = 8; letter_Y_Offset = 0;
                        break;

                    case ('9'): letter_X_Offset = 9; letter_Y_Offset = 0;
                        break;

                    case ('A'): letter_X_Offset = 0; letter_Y_Offset = 1;
                        break;

                    case ('B'): letter_X_Offset = 1; letter_Y_Offset = 1;
                        break;

                    case ('C'): letter_X_Offset = 2; letter_Y_Offset = 1;
                        break;

                    case ('D'): letter_X_Offset = 3; letter_Y_Offset = 1;
                        break;

                    case ('E'): letter_X_Offset = 4; letter_Y_Offset = 1;
                        break;

                    case ('F'): letter_X_Offset = 5; letter_Y_Offset = 1;
                        break;

                    case ('G'): letter_X_Offset = 6; letter_Y_Offset = 1;
                        break;

                    case ('H'): letter_X_Offset = 7; letter_Y_Offset = 1;
                        break;

                    case ('I'): letter_X_Offset = 8; letter_Y_Offset = 1;
                        break;

                    case ('J'): letter_X_Offset = 9; letter_Y_Offset = 1;
                        break;

                    case ('K'): letter_X_Offset = 10; letter_Y_Offset = 1;
                        break;

                    case ('L'): letter_X_Offset = 11; letter_Y_Offset = 1;
                        break;

                    case ('M'): letter_X_Offset = 0; letter_Y_Offset = 2;
                        break;

                    case ('N'): letter_X_Offset = 1; letter_Y_Offset = 2;
                        break;

                    case ('O'): letter_X_Offset = 2; letter_Y_Offset = 2;
                        break;

                    case ('P'): letter_X_Offset = 3; letter_Y_Offset = 2;
                        break;

                    case ('Q'): letter_X_Offset = 4; letter_Y_Offset = 2;
                        break;

                    case ('R'): letter_X_Offset = 5; letter_Y_Offset = 2;
                        break;

                    case ('S'): letter_X_Offset = 6; letter_Y_Offset = 2;
                        break;

                    case ('T'): letter_X_Offset = 7; letter_Y_Offset = 2;
                        break;

                    case ('U'): letter_X_Offset = 8; letter_Y_Offset = 2;
                        break;

                    case ('V'): letter_X_Offset = 9; letter_Y_Offset = 2;
                        break;

                    case ('W'): letter_X_Offset = 10; letter_Y_Offset = 2;
                        break;

                    case ('X'): letter_X_Offset = 11; letter_Y_Offset = 2;
                        break;

                    case ('Y'): letter_X_Offset = 0; letter_Y_Offset = 3;
                        break;

                    case ('Z'): letter_X_Offset = 1;  letter_Y_Offset = 3;
                        break;

                    case ('!'): letter_X_Offset = 2; letter_Y_Offset = 3;
                        break;

                    case ('@'): letter_X_Offset = 3; letter_Y_Offset = 3;
                        break;

                    case ('#'): letter_X_Offset = 4; letter_Y_Offset = 3;
                        break;

                    case ('$'): letter_X_Offset = 5; letter_Y_Offset = 3;
                        break;

                    case ('%'): letter_X_Offset = 6; letter_Y_Offset = 3;
                        break;

                    case ('^'): letter_X_Offset = 7; letter_Y_Offset = 3;
                        break;

                    case ('&'): letter_X_Offset = 8; letter_Y_Offset = 3;
                        break;

                    case ('*'): letter_X_Offset = 9; letter_Y_Offset = 3;
                        break;

                    case ('('): letter_X_Offset = 10; letter_Y_Offset = 3;
                        break;

                    case (')'): letter_X_Offset = 11; letter_Y_Offset = 3;
                        break;

                    default:
                        letter_X_Offset = 10;
                        letter_Y_Offset = 0;
                        break;
                }

                //create a rectangle on the correct location of the image(do math)
                location_Rect = new Rectangle(x_Offset + (letter_Width*letter_X_Offset), y_Offset+(letter_Height*letter_Y_Offset), letter_Width, letter_Height);
            }

            public void translateLetter(int x, int y)
            {
                screen_Rect.X += x;
                screen_Rect.Y += y;
            }

            public void Draw(SpriteBatch spriteBatch)
            {
                spriteBatch.Draw(HUD_Items, screen_Rect, location_Rect, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
            }
        }
    }
}
