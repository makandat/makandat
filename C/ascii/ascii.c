#include <stdio.h>

/* ASCII コードを表示する。 */
int main()
{
  int i, j;
  unsigned char c;

  /* 0x00 から 0xff まで繰り返す。*/
  for (i = 0; i < 16; i++) {
    for (j = 0; j < 16; j ++) {
      /* 現在のコードを得る。*/
      c = (unsigned char)((16 * i + j) & 0xff);
      /* 表示できない文字を空白に置き換え */
      if ((c >= 0x00 && c <= 0x20) ||(c >= 0x80 && c <= 0x9f)) {
         c = 0x20;
      }
      /* コードを文字列として表示する */
      printf("%03d %x%x %c ", 16 * i + j, i, j, (char)c);
      /* ８文字ごとに改行するため。*/
      if (j == 7)
        printf("\n");
    }
    /* ８文字ごとに改行するため。*/
    printf("\n");
  }
}

