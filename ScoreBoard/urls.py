from rest_framework.routers import SimpleRouter
from ScoreBoard.views import ScoreViewSet

router = SimpleRouter()
router.register('', ScoreViewSet)

urlpatterns = router.urls