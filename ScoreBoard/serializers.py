from rest_framework.serializers import ModelSerializer
from ScoreBoard.models import Score


class ScoreSerializer(ModelSerializer):
    class Meta:
        model = Score
        fields = ['nickname', 'points', 'created_at']
        filter