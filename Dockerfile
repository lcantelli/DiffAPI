FROM lcantelli/cucumber

RUN rm -rf /features

COPY /features features