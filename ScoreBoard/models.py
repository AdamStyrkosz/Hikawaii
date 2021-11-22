from django.db import models

# Create your models here.

class Score(models.Model):
    nickname = models.CharField(max_length=16)
    points = models.IntegerField()
    created_at = models.DateTimeField(auto_now_add=True)

    def __str__(self):
        return f"{self.nickname} : {str(self.points)}"
